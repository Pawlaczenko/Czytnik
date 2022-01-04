const mobileNavigationHandler = () => {
  let isNavigationVisible = false;
  const mobileNavigation = document.querySelector('.js-navigation__mobile');
  const blur = document.querySelector('.js-blur');
  const burger = document.querySelector('.js-navigation__burger');
  const burgerClose = document.querySelector('.js-navigation__mobile-close');

  burger.addEventListener('click', () => {
    if (!isNavigationVisible) {
      mobileNavigation.classList.add('navigation__mobile-is-visible');
      blur.classList.add('blur__is-visible');
    } else {
      mobileNav.classList.remove('navigation__mobile-is-visible');
      blur.classList.remove('blur__is-visible');
    }

    isNavigationVisible = !isNavigationVisible;
  });

  [burgerClose, blur].forEach(el => {
    el.addEventListener('click', () => {
      mobileNavigation.classList.remove('navigation__mobile-is-visible');
      blur.classList.remove('blur__is-visible');

      isNavigationVisible = false;
    });
  });
};
