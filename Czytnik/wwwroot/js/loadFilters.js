﻿const categoriesFilterContainer = document.querySelector(".js-categories-filter");
const languagesFilterContainer = document.querySelector(".js-languages-filter");

const categoriesFilterSearch = document.querySelector(".js-categories-filter-search");
const languagesFilterSearch = document.querySelector(".js-languages-filter-search");

const renderCategories = (categories) => {
    let template = '';
    categories.forEach(item => {
        template += `
            <div class="filter__results-item">
                <input class="filter__results-checkbox" type="radio" name="category" id="${item.name}" value="${item.id}">
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
                <input class="filter__results-checkbox" type="radio" name="category" id="${language.name}" value="${language.id}">
                <label class="filter__results-label" for="${language.name}">${language.name}</label>
            </div>`
    });

    languagesFilterContainer.innerHTML = template;
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
    renderLanguages(languages)
    
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