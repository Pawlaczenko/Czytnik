(function () {
    const quantityInputs = document.querySelectorAll('.js-cart-quantity-input');
    quantityInputs.forEach(input => {
        input.addEventListener('focusout', (e) => {
            const book = e.target.dataset.book;
            const quantity = e.target.value;

            $.ajax({
                type: 'PATCH',
                url: '/Cart/UpdateQuantity',
                data: { bookId: book, quantity: quantity },
                dataType: 'json',
                success: function () {
                },
                error: function (err) {
                    console.log(err);
                }
            });
        })
    })

    const cart = document.querySelector('.js-cart-container');
    setCartPromotion();

    cart.addEventListener('click', (e) => {
        const incrementButton = e.target.closest('.js-cart-quantity-increment');
        const decrementButton = e.target.closest('.js-cart-quantity-decrement');
        const deleteButton = e.target.closest('.js-cart-item-delete');

        if (incrementButton) incrementQuantity(incrementButton);
        else if (decrementButton) decrementQuantity(decrementButton);
        else if (deleteButton) deleteItem(deleteButton);
        
    })

    const deleteItem = (button) => {
        const book = button.dataset.book;

        $.ajax({
            type: 'DELETE',
            url: '/Cart/DeleteItem',
            data: { bookId: book },
            dataType: 'json',
            success: function (reviews) {
                const cartItem = button.closest('.js-cart-item');
                cart.removeChild(cartItem);
                const cartItemsCountElement = document.querySelector('.js-cart-quantity');
                cartItemsCountElement.dataset.count -= 1;
                cartItemsCountElement.innerText = `${cartItemsCountElement.dataset.count} przedmioty`;
                setCartPrice();
                setCartPromotion();
            },
            error: function (err) {
                console.log(err);
            }
        });
    }

    const decrementQuantity = (button) => {
        const quantityInputItem = button.nextElementSibling;

        if (quantityInputItem.value <= 1) return;
        quantityInputItem.focus();
        quantityInputItem.value--;
        const cartItem = button.closest('.js-cart-item');
        const price = parseFloat(cartItem.querySelector('.js-cart-item-price').innerText.replace(',', '.'));
        const promotionItem = cartItem.querySelector('.js-cart-item-promotion');
        const defaultPromotion = promotionItem.dataset.defaultDiscount.replace(',', '.');

        const fullPrice = calculateFullPrice(price, quantityInputItem.value)
        const newPromotion = calculateCartItemPromotion(defaultPromotion, quantityInputItem.value)
        const cartFullPriceItem = cartItem.querySelector('.js-cart-item-full-price');
        promotionItem.dataset.discount = newPromotion;
        cartFullPriceItem.innerText = `${fullPrice} zł`;
        setCartPrice();
        setCartPromotion();
    }

    const incrementQuantity = (button) => {
        const quantityInputItem = button.previousElementSibling;

        quantityInputItem.focus();
        quantityInputItem.value++;
        const cartItem = button.closest('.js-cart-item');
        const price = parseFloat(cartItem.querySelector('.js-cart-item-price').innerText.replace(',', '.'));
        const promotionItem = cartItem.querySelector('.js-cart-item-promotion');
        const defaultPromotion = promotionItem.dataset.defaultDiscount.replace(',', '.');

        const fullPrice = calculateFullPrice(price, quantityInputItem.value)
        const newPromotion = calculateCartItemPromotion(defaultPromotion, quantityInputItem.value)
        const cartFullPriceItem = cartItem.querySelector('.js-cart-item-full-price');
        promotionItem.dataset.discount = newPromotion;
        cartFullPriceItem.innerText = `${fullPrice} zł`;
        setCartPrice();
        setCartPromotion();
    }

    const calculateFullPrice = (price, quantity) => {
        return `${(Math.round(price * quantity * 100) / 100).toFixed(2)}`.replace('.', ',');
    }

    const calculateCartItemPromotion = (discount, quantity) => {
        return Math.round(discount * quantity * 100) / 100;
    }

    const setCartPrice = () => {
        const prices = Array.from(document.querySelectorAll('.js-cart-item-full-price')).map(el => parseFloat(el.innerText.replace(',', '.')));
        const sum = `${(Math.round(prices.reduce((sum, price) => sum + price, 0) * 100) / 100).toFixed(2)}`.replace('.', ',');

        document.querySelector(".js-cart-price").innerText = `${sum} zł`;
    }

    function setCartPromotion(){
        const promotions = Array.from(document.querySelectorAll('.js-cart-item-promotion')).map(el => parseFloat(el.dataset.discount.replace(',', '.')));
        const sum = `${(Math.round(promotions.reduce((sum, promotion) => sum + promotion, 0) * 100) / 100).toFixed(2)}`.replace('.', ',');

        document.querySelector(".js-cart-promotion").innerText = `-${sum} zł`;
    }
})();