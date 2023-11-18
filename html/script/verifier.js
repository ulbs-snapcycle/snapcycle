function getQueryParameter(name) {
    const urlSearchParams = new URLSearchParams(window.location.search);
    return urlSearchParams.get(name);
}

const userId = getQueryParameter('userId');
const image = document.getElementById("image");
const buttons = document.getElementsByClassName("button");
const trash = document.getElementById("trash");
let currentUserImageId, currentImageId, currentUserId;
let trashType;

function buttonClick(index){
    let button = buttons[index];
    let colors = ["black","red","blue","gold"]

    button.style.backgroundColor = colors[index];
    setTimeout(function() {
        button.style.backgroundColor = "#64bd43";
    }, 300);
    updateScoreAndImages(index);
}

function updateImage(){
    const baseUrl = "http://localhost:5068/api/v1/";
    const userImagesUrl = baseUrl + "Images/";

    fetch(userImagesUrl + `image_file/${currentImageId}`, {
        method: 'GET'
    })
        .then(response => response.blob())
        .then(blob => {
            const reader = new FileReader();
            reader.onloadend = function() {
                image.src = reader.result;
            };
            reader.readAsDataURL(blob);
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}

function updateTrashType(){
    const baseUrl = "http://localhost:5068/api/v1/";
    const userImagesUrl = baseUrl + "UserImages/";

    fetch(userImagesUrl + `user_image/${currentUserImageId}`, {
        method: 'GET',
    })
        .then(response => { return response.json(); })
        .then(data => {
            trashType = data.trashType;

            switch (trashType){
                case 0:
                    trash.innerHTML = "Paper";
                    break;
                case 1:
                    trash.innerHTML = "Plastic";
                    break;
                case 2:
                    trash.innerHTML = "Glass";
                    break;
                case 3:
                    trash.innerHTML = "Metal";
                    break;
                case 4:
                    trash.innerHTML = "Organic";
                    break;
                case 5:
                    trash.innerHTML = "Electronic";
                    break;
            }
        });
}

function getNextImage(){
    const baseUrl = "http://localhost:5068/api/v1/";
    const userImagesUrl = baseUrl + "UserImages/";

    fetch(userImagesUrl + "all", {
        method: 'GET'
    })
        .then(response => {
            if(response.status === 404) {
                // NO MORE IMAGES CASE
            }
            else return response.json();
        })
        .then(data =>{
            currentUserImageId = data[0].id;
            currentUserId = data[0].userId;
            currentImageId = data[0].imageId;
            updateTrashType();
            updateImage();
        });
}

function updateScoreAndImages(index){
    const baseUrl = "http://localhost:5068/api/v1/";
    const usersUrl = baseUrl + "Users/";
    const imagesUrl = baseUrl + "Images/";
    const userImagesUrl = baseUrl + "UserImages/";

    const updateScoreHeaders = {
        id: currentUserId,
        result: index,
        trash: trashType
    }

    fetch(usersUrl + "update_score", {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updateScoreHeaders)
    })
        .then(response => { return response.json(); })
        .then(data => console.log(data));

    fetch(userImagesUrl + `delete/${currentUserImageId}`, {
        method: 'DELETE'
    })
        .then(response => { return response.json(); })
        .then(data => {
            console.log(data);
            fetch(imagesUrl + `delete/${currentImageId}`, {
                method: 'DELETE'
            });
            getNextImage();
        });
}

getNextImage();