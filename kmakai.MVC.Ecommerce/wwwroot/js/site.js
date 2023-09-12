const testButton = document.querySelector('.test-btn');

if (testButton) testButton.addEventListener('click', async () => {
    const response = await fetch('order/create');
    const data = await response.json();
    console.log(data);
});



// Cart functions and controls
const addToCartForms = document.querySelectorAll('.add-product-form');
const cart = document.querySelector('#shopping-cart-offcanvas');

if (cart) displayCart();

if (addToCartForms) {
    addToCartForms.forEach(form => {
        form.addEventListener("submit", (e) => {
            e.preventDefault();
            const formData = new FormData(form);
            const name = formData.get("productName");
            const price = formData.get("productPrice");
            const productId = formData.get("productId");

            console.log(name, price, productId);
            addToCart({ price, name, productId, quantity: 1 });


            displayCart();
        })
    });
}

function addToCart(product) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    cart.push(product);

    localStorage.setItem('cart', JSON.stringify(cart));
}

function displayCart() {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    let cartItems = '';

    cart.forEach((item) => {
        cartItems += `
             <li class="">
                <div class="product-cart-card">
                    <div class="product-info">
                        <a href="#" class="product-name">
                            ${item.name}
                        </a>
                        <div class="product-price-info">
                            <span class="price-title">Price:</span>
                            <span class="product-price">
                               $${item.price}
                            </span>
                        </div>
                        <div class="product-quantity-actions">
                            <span class="quantity-title">Quantity:</span>
                            <i class="bi bi-dash-circle" id="qauntity-sub" data-id="${item.productId}"></i>
                            <span class="product-quantity">${item.quantity}</span>
                            <i class="bi bi-plus-circle" id="quantity-add" data-id="${item.productId}"></i>
                        </div>

                        <span href="#" class="product-remove text-secondary" data-id="${item.productId}"> remove </span>
                    </div>
                    <img src="https://fakestoreapi.com/img/71-3HjGNDUL._AC_SY879._SX._UX._SY._UY_.jpg" class="d-none d-md-block"/>

                </div>
            </li>
        `;
    });

    cartElement = document.querySelector('#shopping-cart-list');
    cartElement.innerHTML = '';
    cartElement.insertAdjacentHTML('beforeend', cartItems);

    cartAmount = document.querySelector('#cart-amount-badge');
    cartAmount.innerHTML = cart.length;

    const productRemoveButtons = document.querySelectorAll('.product-remove');

    productRemoveButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            console.log('remove', button.dataset.id);
            let cart = JSON.parse(localStorage.getItem('cart')) || [];
            if (cart.length > 0) {
                cart = cart.filter(item => item.productId != button.dataset.id);
            }
            localStorage.setItem('cart', JSON.stringify(cart));

            displayCart();
        });
    });

    const quantityAddButtons = document.querySelectorAll('#quantity-add');

    quantityAddButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            let = cart = JSON.parse(localStorage.getItem('cart')) || [];
            if (cart.length > 0) {
                cart = cart.map(item => {
                    if (item.productId == button.dataset.id) {
                        item.quantity++;
                    }
                    return item;
                });
            };

            localStorage.setItem('cart', JSON.stringify(cart));

            displayCart();
        });
    });

    const quantitySubButtons = document.querySelectorAll('#qauntity-sub');

    quantitySubButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            let = cart = JSON.parse(localStorage.getItem('cart')) || [];
            if (cart.length > 0) {
                cart = cart.map(item => {
                    if (item.productId == button.dataset.id && item.quantity > 1) {
                        item.quantity--;
                    }
                    return item;
                });
            };

            localStorage.setItem('cart', JSON.stringify(cart));

            displayCart();
        });
    });

    calculateCheckout();
};

//const productRemoveButtons = document.querySelectorAll('.product-remove');

//productRemoveButtons.forEach(button => {
//    button.addEventListener('click', (e) => {
//        console.log('remove', button.dataset.id);
//        let cart = JSON.parse(localStorage.getItem('cart')) || [];
//        if (cart.length > 0) {
//            cart = cart.filter(item => item.productId != button.dataset.id);
//        }
//        localStorage.setItem('cart', JSON.stringify(cart));

//        displayCart();
//    });
//});

const checkoutButton = document.querySelector('#checkout-btn');

if (checkoutButton) {
    checkoutButton.addEventListener('click', async () => {
        console.log('checkout');
        let cart = JSON.parse(localStorage.getItem('cart'));

        const response = await fetch('/order/checkout', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(cart),
        }).catch(error => console.error(error));

        if (response.ok) {
            console.log('Checkout successful!');
        } else {
            console.log('Error during checkout:', response.status);
        }

        const data = await response.json();
        console.log(data);
    });
};
// Cart functions and controls

// check out functions and controls



function calculateCheckout() {
    const checkoutSubtotal = document.querySelector('.checkout-subtotal-price');
    const checkoutTax = document.querySelector('.checkout-tax-price');
    const checkoutTotal = document.querySelector('.checkout-total-price');

    if (checkoutSubtotal && checkoutTax && checkoutTotal) {
        let cart = JSON.parse(localStorage.getItem('cart')) || [];
        let subtotal = 0;
        let tax = 0;
        let total = 0;

        cart.forEach(item => {
            subtotal += item.price * item.quantity;
        });

        tax = subtotal * 0.08;
        total = subtotal + tax;

        checkoutSubtotal.textContent = `$${subtotal.toFixed(2)}`;
        checkoutTax.textContent = `$${tax.toFixed(2)}`;
        checkoutTotal.textContent = `$${total.toFixed(2)}`;
    }
}