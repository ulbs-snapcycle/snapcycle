function previewImage() {
    var previewContainer = document.getElementById('preview_container');
    var trashImageInput = document.getElementById('trash_image');
    var trashTypeDropdown = document.getElementById('trash_type');
    var uploadButton = document.getElementById('upload_button');

    // Clear previous preview
    previewContainer.innerHTML = '';

    // Display selected image
    if (trashImageInput.files && trashImageInput.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var image = new Image();
            image.src = e.target.result;
            image.style.maxWidth = '100%';
            previewContainer.appendChild(image);

            // Access selected trash type
            var selectedTrashType = trashTypeDropdown.value;
            console.log('Selected Trash Type:', selectedTrashType);
        };
        reader.readAsDataURL(trashImageInput.files[0]);

        uploadButton.hidden = false;
        uploadButton.enable = true;
    }
    else
    {
        // No image selected, disable the upload button
        uploadButton.hidden = true;
        uploadButton.enable = false;
    }
}

function UploadImage()
{
    var uploadButton = document.getElementById('upload_button');
    var uploadTextConfirmation = document.getElementById('upload_confirmation_text');

    console.log('Image uploaded')
    // No image selected, disable the upload button
    uploadButton.hidden = true;
    uploadButton.enable = false;
    uploadButton.remove();
    uploadTextConfirmation.hidden = false;
}