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
            const imageUrl = formData.get("productImageUrl");

            console.log(name, price, productId);
            addToCart({ price, name, productId, quantity: 1, imageUrl });


            displayCart();
        })
    });
}

function addToCart(product) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    const existingProduct = cart.find(item => item.productId == product.productId);

    if (existingProduct) {
        cart = cart.map(item => {
            if (item.productId == product.productId) {
                item.quantity++;
            }
            return item;
        });
    } else {
        cart.push(product);
    }

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
                        <a href="/product/${item.productId}" class="product-name">
                            ${item.name}
                        </a>
                        <div class="product-price-info">
                            <span class="price-title fw-semibold">Price:</span>
                            <span class="product-price text-success fw-medium">
                               $${item.price}
                            </span>
                        </div>
                        <div class="product-quantity-actions">
                            <span class="quantity-title">Quantity:</span>
                            <i class="bi bi-dash-circle" id="qauntity-sub" data-id="${item.productId}"></i>
                            <span class="product-quantity">${item.quantity}</span>
                            <i class="bi bi-plus-circle" id="quantity-add" data-id="${item.productId}"></i>
                        </div>

                        <span href="#" class="product-remove text-secondary" data-id="${item.productId}"> Remove </span>
                    </div>
                    <img src="${item.imageUrl}" class="d-none d-md-block"/>

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

// Search functions and controls

const searchBox = document.querySelector('#search-box');
const searchResults = document.querySelector('.search-container');
const searchCategory = document.querySelector('#search-category');
const searchButton = document.querySelector('#search-button');

async function searchProducts(e) {
    const searchValue = e.target.value;
    const searchCategoryValue = searchCategory.value;

    console.log(searchValue, searchCategoryValue);

    if (searchValue.length > 0) {
        const response = await fetch(`/products/search?searchValue=${searchValue}&searchCategory=${searchCategoryValue}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        }).catch(error => console.error(error));

        if (response.ok) {
            const data = await response.json();
            displaySearchResults(data);
        } else {
            console.log('Error during search:', response.status);
        }
    } else {
        searchResults.innerHTML = '';
        document.querySelectorAll(".row").forEach(row => row.classList.remove("d-none"))
    }

}

searchBox && searchBox.addEventListener('keyup', async (e) => searchProducts(e));
searchButton && searchButton.addEventListener('click', () => {
    searchBox.value = '';
    searchCategory.value = 0;
    searchProducts({ target: { value: '' } });
});

function displaySearchResults(data) {
    let results = '';

    data.forEach(item => {
        results += `
             <div class="product-card border border-3">
                    <img src="${item.imageUrl}" alt="" class="card-img product-img" />
                    <div class="product-card-info">
                        <span class="card-title">
                            <a href="/product/${item.productId}">
                                ${item.name}
                            </a>
                        </span>
                        <div class="price-rating d-flex">
                            <span class="">
                                ${item.price}
                            </span>
                            <span class="ms-auto">
                                ${item.rating}

                                <i class="bi bi-star-fill star"></i>
                            </span>
                        </div>
                        <span>In ${item.category.name}</span>
                        <div class="mt-auto">
                            <form class="add-product-form">
                                <input type="hidden" name="productId" value="${item.productId}" />
                                <input type="hidden" name="productName" value="${item.name}" />
                                <input type="hidden" name="productPrice" value="${item.price}" />
                                <input type="hidden" name="productImageUrl" value="${item.imageUrl}" />
                                <button type="submit" class="btn btn-success">Add to cart</button>
                            </form>
                        </div>


                    </div>
                </div>
        `;


    });

    document.querySelectorAll(".row").forEach(row => row.classList.add("d-none"))

    searchResults.innerHTML = "";
    searchResults.insertAdjacentHTML('beforeend', results);

    const addToCartForms = document.querySelectorAll('.add-product-form');
    if (addToCartForms) {
        addToCartForms.forEach(form => {
            form.addEventListener("submit", (e) => {
                e.preventDefault();
                const formData = new FormData(form);
                const name = formData.get("productName");
                const price = formData.get("productPrice");
                const productId = formData.get("productId");
                const imageUrl = formData.get("productImageUrl");

                console.log(name, price, productId);
                addToCart({ price, name, productId, quantity: 1, imageUrl });


                displayCart();
            })
        });
    }

}