const userId = getQueryParameter('userId');
function getQueryParameter(name) {
    const urlSearchParams = new URLSearchParams(window.location.search);
    return urlSearchParams.get(name);
}

const image = document.getElementById("image");
let upload = document.getElementById("uploadImage");
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
            score.innerHTML = `Your Score : ${data.score}`;
        });
}

function uploadImage(){
    const baseUrl = "http://localhost:5068/api/v1/";
    const imagesUrl = baseUrl + "Images/";
    const userImagesUrl = baseUrl + "UserImages/";
    const fileInput = document.getElementById('uploadImage');
    const file = fileInput.files[0];

    if (file) {
        const formData = new FormData();
        formData.append('file', file);

        fetch(imagesUrl + "create", {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(data => {
                const headers = {
                    userId: parseInt(userId),
                    imageId: data.id,
                    trashType: parseInt(trash.value)
                }

                fetch(userImagesUrl + "create", {
                    method: 'POST',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(headers)
                })
                    .then(response => {return response.json()})
                    .then(data => {
                        console.log(data);
                    });
            });
    } else {
        console.error('No file selected');
    }
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