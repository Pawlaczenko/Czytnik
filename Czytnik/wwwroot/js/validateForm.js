const validFormTypes = {
    'EMAIL': 1,
    'USERNAME': 4,
    'PASSWORD': 2,
    'REPEAT_PASSWORD': 3
}

const removeError = (input, type) => {
    if(document.getElementById(`errorMessage${type}`)) form.removeChild(document.getElementById(`errorMessage${type}`));
    input.classList.remove("form__input--error");
}

const generateErrorMessegeElement = (message,type) => {
    const errorMessage = document.createElement("p");
    errorMessage.textContent = message;
    errorMessage.classList="form__errorMessage";
    errorMessage.id = `errorMessage${type}`;
    return errorMessage;
}

const addError = (input,message, type) => {
    if(!input.classList.contains("form__input--error")) input.classList.add("form__input--error");
    if(!document.getElementById(`errorMessage${type}`)) form.insertBefore(generateErrorMessegeElement(message, type),input);
}

const validateEmail = (input) => {
    const validRegex =  /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if(input.value.match(validRegex)){
        removeError(input,validFormTypes.EMAIL);
        return true;
    } else {
        addError(input,"Podaj poprawny email",validFormTypes.EMAIL);
        return false;
    }
}

const validatePassword = (input) => {
    const validRegex =  /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;
    if(input.value.match(validRegex)){
        removeError(input,validFormTypes.PASSWORD);
        return true;
    } else {
        addError(input,"Hasło musi posiadać minimum 8 znaków, jedną małą i dużą literę i jedną cyfrę.",validFormTypes.PASSWORD);
        return false;
    }
}

const validateSecondPassword = (firstPassword, secondPassword) => {
    if(firstPassword.value===secondPassword) {
        removeError(firstPassword,validFormTypes.REPEAT_PASSWORD);
        return true;
    } else {
        addError(firstPassword,"Hasła muszą być identyczne",validFormTypes.REPEAT_PASSWORD);
        return false;
    }
}

const validateUsername = (input) => {
    const validRegex =  /^[a-zA-Z0-9].{5,}$/;
    if(input.value.match(validRegex)){
        removeError(input,validFormTypes.USERNAME);
        return true;
    } else {
        addError(input,"Nazwa użytkownika musi posiadać minimum 8 znaków, literę lub cyfrę",validFormTypes.USERNAME);
        return false;
    }
}