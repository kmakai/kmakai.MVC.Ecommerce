const testButton = document.querySelector('.test-btn');

testButton.addEventListener('click', async () => {
    const response = await fetch('order/create');
    const data = await response.json();
    console.log(data);
});