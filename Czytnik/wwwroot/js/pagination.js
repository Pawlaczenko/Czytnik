const pagination = document.querySelector(".js-pagination");

pagination.addEventListener('click', function(e) {
    e.preventDefault();
    if (e.target.matches(".pagination__item--text")) return;

    const page = e.target.innerText;

    if (this.dataset.page == e.target.innerText) return;

    const urlParams = new URLSearchParams(window.location.search);
    urlParams.set('Page', page);

    window.location.search = urlParams;
})