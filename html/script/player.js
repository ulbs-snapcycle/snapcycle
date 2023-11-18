const userId = getQueryParameter('userId');
function getQueryParameter(name) {
    const urlSearchParams = new URLSearchParams(window.location.search);
    return urlSearchParams.get(name);
}

const image = document.getElementById("image");
const upload = document.getElementById("uploadImage");
const button = document.getElementById("uploadButton");
const trash = document.getElementById('trash-list');
const score = document.getElementById("score");

function previewImage() {
    if (upload.files && upload.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            image.src = e.target.result;
        };
        reader.readAsDataURL(upload.files[0]);
    }
}

function updateScore(){
    const baseUrl = "http://localhost:5068/api/v1/";
    const usersUrl = baseUrl + "Users/";

    fetch(usersUrl + `user/${userId}`, {
        method: 'GET'
    })
        .then(response => {return response.json()})
        .then(data => {
            console.log(data);
            score.innerHTML = "Your Score : ${data.score}";
        });
}

function uploadImage(){

}

function buttonClick() {
    button.style.backgroundColor = "beige";
    button.style.color = "#3d7007";
    setTimeout(function () {
        button.style.backgroundColor = "#3d7007";
        button.style.color = "beige";
    }, 500);

    uploadImage();
}

getQueryParameter();
updateScore();