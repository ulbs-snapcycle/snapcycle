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

function invalidCredentials(){
    warning.innerHTML = "Incorrect email or password!";
    displayWarning();
}

function login(){
    if(isStringNullOrWhitespace(email.value) || isStringNullOrWhitespace(password.value)){
        warning.innerHTML = "Please complete all fields!";
        displayWarning();
        return;
    }

    const url = "http://localhost:5068/api/v1/Users/login";
    const data = {
        email: email.value,
        password: password.value
    };

    fetch(url, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
        .then(response => {
            if(response.status === 401 || response.status === 404) {
                invalidCredentials();
                throw new Error(`Invalid credentials`);
            }
            else return response.json();
        })
        .then(data => {
            if(data.type === 1){
                const query = { userId: data.id };
                const queryString = Object.entries(query).map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`).join('&');
                const redirectUrl = 'player.html' + '?' + queryString;

                window.location.href = redirectUrl;
            }
            if(data.type === 2){
                const query = { userId: data.id };
                const queryString = Object.entries(query).map(([key, value]) => `${encodeURIComponent(key)}=${encodeURIComponent(value)}`).join('&');
                const redirectUrl = 'verifier.html' + '?' + queryString;

                window.location.href = redirectUrl;
            }
        });
}