(function () {
  const storage = window.localStorage;

   let items = '';
   if (storage.getItem('type') == 'single') {
     items = storage.getItem('singleItem');
   } else {
     items = storage.getItem('cartItems');
   }

  makeOrder();

  function makeOrder() {
    $.ajax({
      url: '/Checkout/Order',
      type: 'POST',
      data: { products: items, type: storage.getItem('type') },
      datatype: 'json',
      success: function () {
        console.log('success');
        clearCart();
      },
    });
  }

  function clearCart() {
    if (storage.getItem('type') == 'single') {
      storage.removeItem('singleItem');
      storage.removeItem('singleFull');
    } else {
      storage.removeItem('cartItems');
      storage.removeItem('cartFull');

      $.ajax({
        url: '/Cart/Clear',
        type: 'DELETE',
        datatype: 'json',
        success: function () {
          console.log('success');
        },
      });
    }
    storage.removeItem('type');


  }
})();