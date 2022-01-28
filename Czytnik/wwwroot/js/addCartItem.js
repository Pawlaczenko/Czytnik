const addCartItem = (button) => {
    const book = button.dataset.book

    $.ajax({
        type: 'POST',
        url: '/Cart/AddItem',
        data: { bookId: book},
        dataType: 'json',
        success: function (cartItem) {
            const navCartQuantityItem = document.querySelector('.js-nav-cart-quantity');
            navCartQuantityItem.innerText = navCartQuantityItem.innerText*1+1
        },
        error: function (err) {
            //console.log(err);
        }
    });
    button.classList.add("books-carousel__button--disabled");
    button.innerHTML = 'Dodano do koszyka';
    button.disabled = true;
}

const booksContainer = document.querySelector('.js-books-container');

booksContainer.addEventListener('click', (e) => {
    
    const addItemButton = e.target.closest('.js-cart-item-add');
    if (addItemButton) {
        e.preventDefault();
        addCartItem(addItemButton);
    }
})