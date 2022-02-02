(function() {
  const myStorage = window.localStorage;
  const items = JSON.parse(myStorage.getItem('cartItems')) || new Array();

  const cartQuantityElement = document.querySelector('.js-nav-cart-quantity');
  cartQuantityElement.innerText = items.length;
})();
