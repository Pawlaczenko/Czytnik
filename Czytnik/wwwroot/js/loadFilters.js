const urlSearchParams = new URLSearchParams(window.location.search);
const params = Object.fromEntries(urlSearchParams.entries());

const categoriesFilterContainer = document.querySelector(".js-categories-filter");
const languagesFilterContainer = document.querySelector(".js-languages-filter");

const categoriesFilterSearch = document.querySelector(".js-categories-filter-search");
const languagesFilterSearch = document.querySelector(".js-languages-filter-search");

const sortingSelectItems = document.querySelectorAll(".js-sorting-select > *");
const searchNavigationInput = document.querySelector(".js-search-navigation-input");

const renderCategories = (categories) => {
    let template = '';
    categories.forEach(item => {
        template += `
            <div class="filter__results-item">
                <input class="filter__results-checkbox" type="radio" name="CategoryId" id="${item.name}" value="${item.id}" ${params.CategoryId == item.id && "checked"}>
                <label class="filter__results-label" for="${item.name}">${item.name}</label>
            </div>`
    });

    categoriesFilterContainer.innerHTML = template;
}

const renderLanguages = (languages) => {
    let template = '';
    languages.forEach(language => {
        template += `
            <div class="filter__results-item">
                <input class="filter__results-checkbox" type="radio" name="LanguageId" id="${language.name}" value="${language.id}" ${params.LanguageId == language.id && "checked"}>
                <label class="filter__results-label" for="${language.name}">${language.name}</label>
            </div>`
    });

    languagesFilterContainer.innerHTML = template;
}

const renderPagination = () => {
    const pagination = document.querySelector(".js-pagination");
    const booksQuantity = pagination.dataset.quantity;
    const page = pagination.dataset.page;

    let startPage = page;
    let endPage = Math.ceil(booksQuantity / 30);
    if (page > 4) startPage -= 4;
    else startPage = 1;
    let template = "";

    if (page > 1) template += `
        <button class="pagination__item pagination__item--arrow">
            <svg class="pagination__arrow pagination__arrow--left" viewBox="0 0 6 9">
                <use xlink:href="/assets/svg/sprite.svg#icon-chevron-right"></use>
            </svg>
        </button>
    `;

    if (startPage > 1) template += `
        <button class="pagination__item">1</button>
        <span class="pagination__item pagination__item--text">...</span>
    `

    for (let i = startPage; i < startPage + 8 && i <= endPage; i++) {
        template += `
            <button class="pagination__item ${page == i ? "pagination__item--active" : ""}">${i}</button>
        `
    }

    template += `
        <span class="pagination__item pagination__item--text">z</span>
        <button class="pagination__item">${endPage}</button>
    `

    if (page < endPage) template += `
        <button class="pagination__item pagination__item--arrow">
            <svg class="pagination__arrow" viewBox="0 0 6 9">
                <use xlink:href="/assets/svg/sprite.svg#icon-chevron-right"></use>
            </svg>
        </button>
    `;

    pagination.innerHTML = template;

    console.log(booksQuantity);
}

const setSortingOption = () => {
    sortingSelectItems.forEach(item => {
        if (item.value == params.Sort) item.selected = 'selected';
    })
}

const generateFilters = (data) => {
    const categories = data.categories.map(el => {
        return {
            name: el.category.name,
            id: el.category.id
        }
    });
    const languages = data.languages;


    renderCategories(categories);
    renderLanguages(languages);
    renderPagination();

    setSortingOption();

    searchNavigationInput.value = params.SearchText ? params.SearchText : '';

    categoriesFilterSearch.addEventListener('input', (e) => {
        const searchValue = e.target.value.toLowerCase();
        const filteredCategories = categories.filter(el => el.name.toLowerCase().includes(searchValue));

        renderCategories(filteredCategories);
    })

    languagesFilterSearch.addEventListener('input', (e) => {
        const searchValue = e.target.value.toLowerCase();
        const filteredLanguages = languages.filter(el => el.name.toLowerCase().includes(searchValue));

        renderLanguages(filteredLanguages);
    })
}

const getFiltersData = () => {
    $.ajax({
        type: 'GET',
        url: 'Search/GetAllFilters',
        dataType: 'json',
        success: function (filters) {
            generateFilters(filters);
        },
        error: function (err) {
            alert('error');
        }
    });
}

getFiltersData();