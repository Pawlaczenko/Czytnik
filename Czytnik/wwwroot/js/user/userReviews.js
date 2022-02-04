const reviewsContainer = document.getElementById("userReviews");
const take = 5;
let skip = 0;
let sortBy = "date-desc";

const moreButton = document.getElementById("moreButton");
const sortOptions = document.getElementById("sort");
const reviewsCount = document.getElementById("reviewsCount");

const handleChange = (e) => {
    sortBy = e.target.value;
    skip = 0;
    reviewsContainer.innerHTML="";
    moreButton.disabled = false;
    loadReviews();
}

const getReview = (review) => {
    const markup = `
    <li class="userReview userReview--buttons" data-id="${review.Id}">
        <div class="userReview__column">
            <div class="userReview__header">
                <div>
                    <svg class="userReview__icon icon" viewBox="0 0 32 32">
                        <use xlink:href="/assets/svg/sprite.svg#icon-star"></use>
                    </svg>
                    <span class="userReview__rating">${review.Rating}</span>
                </div> 
                <span class="userReview__date">${review.ReviewDate.slice(0,10)}</span>
            </div>
            <h3 class="userReview__title">${review.BookTitle} - <span class="userReview__author">${review.Authors}</span></h3>
            <p class="userReview__text">
                ${review.ReviewText || ""}
            </p>
        </div>
        <div class="userReview__buttons">
            <button class="button button--secondary">EDYTUJ</button>
            <button class="button button--danger" data-type="delete" data-id="${review.Id}">USUŃ</button>
        </div>
    </li>
    `;

    return markup;
}

const loadReviews = () => {
    displaySpinner();
    $.ajax({
        type: "GET",
        url: "/Reviews/GetAllUserReviews",
        data: {skip: skip, count: take, sortBy: sortBy},
        dataType: 'json',
        success: function (reviews) {
            skip+=take;
            reviews.forEach(r => {
                let content = getReview(r);
                reviewsContainer.innerHTML+=(content);
            });
            hideSpinner();
            if(reviews.length ==0) {
                moreButton.removeEventListener('click',loadReviews);
                moreButton.disabled = true;
            }
        },
        error: function (emp) {
            alert('Coś poszło nie tak :(');
        }
    });
}

const updateReviewsCount = () => {
    let amount = parseInt(reviewsCount.innerHTML) -1;
    reviewsCount.innerHTML = amount;
}

const deleteReview = (id, element) => {
    $.ajax({
        type: "DELETE",
        url: "/Reviews/Delete",
        data: {Id: id},
        dataType: 'json',
        success: function (result) {
            element.innerHTML="Pomyślnie usunięto recenzje";
            updateReviewsCount();
        },
        error: function (emp) {
            alert("Coś poszło nie tak");
        }
    });
}

moreButton.addEventListener('click',loadReviews);
sortOptions.addEventListener('change',handleChange);
loadReviews();

reviewsContainer.addEventListener('click',e=>{
    if(e.target.dataset.type === "delete"){
        let id = parseInt(e.target.dataset.id);
        e.target.disabled = true;
        deleteReview(id,e.target.closest('.userReview'));
    }
});