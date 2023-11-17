const email = document.getElementById("email");
const password = document.getElementById("password");
const warning = document.getElementById("warning");

function isStringNullOrWhitespace(str) {
    return !str || str.trim() === '';
}

function displayWarning(){
    warning.style.display = "block";
    setTimeout(function() {
        warning.style.opacity = 1;
    }, 1);
}

function login(){
    if(isStringNullOrWhitespace(email.value) || isStringNullOrWhitespace(password.value)){
        warning.innerHTML = "Please complete all fields!";
        displayWarning();
        return;
    }

    const url = "http://localhost:5068/api/v1/Users/login";
    let headers = new Headers();
    headers.append("Content-Type", "application/json");
    headers.append("Email", "a");
    headers.append("Password", "a");
    console.log(headers);

    fetch(url, {
        method: 'GET',
        headers: {
            "Content-Type": "application/json",
            "Email": "a",
            "Password": "a"
        }
    })
        .then(response => { return response.json(); })
        .then(data => console.log(data));
}