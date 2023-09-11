const testButton = document.querySelector('.test-btn');

if (testButton) testButton.addEventListener('click', async () => {
    const response = await fetch('order/create');
    const data = await response.json();
    console.log(data);
});



// Cart functions and controls
const addToCartForms = document.querySelectorAll('.add-product-form');
const cart = document.querySelector('#shopping-cart-offcanvas');

if (addToCartForms) {
    addToCartForms.forEach(form => {
        form.addEventListener("submit", (e) => {
            e.preventDefault();
            const formData = new FormData(form);
            const productName = formData.get("productName");
            const productPrice = formData.get("productPrice");
            const productId = formData.get("productId");

            console.log(productName, productPrice, productId);
            addToCart({ productName, productPrice, productId });

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

    cart.forEach(item => {
        cartItems += `
            <tr class="">
                    <td>
                        ${item.productName}
                    </td>
                    <td>
                        ${item.productPrice}
                    </td>
                    <td class="d-flex flex-row justify-content-around align-items-center">
                        <i class="bi bi-dash-circle"></i>
                        <span class="amount">$ ${item.productPrice}</span>
                        <i class="bi bi-plus-circle"></i>
                    </td>
                </tr>
        `;
    });

    cartElement = document.querySelector('#shopping-cart-body');
    cartElement.textContent = '';
    cartElement.insertAdjacentHTML('beforeend', cartItems);
}