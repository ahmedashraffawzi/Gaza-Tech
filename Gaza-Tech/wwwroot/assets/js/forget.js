let inputField = document.querySelector(".login-inputs input[type='email']")
let form = document.querySelector(".form-sub")

form.onsubmit = () => {
    alert(`We will send an email to: ${inputField.value}`);
}
