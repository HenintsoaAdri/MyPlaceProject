$(document).ready(function () {
    var wrapper = document.querySelector('div.image-wrapper');
    wrapper.addEventListener('dragenter', stop_prevent);
    wrapper.addEventListener('dragover', stop_prevent);
    wrapper.addEventListener('drop', onDrop);
    function stop_prevent(e) {
        e.stopPropagation();
        e.preventDefault();
    }
    function onDrop(e) {
        stop_prevent(e);
        setImage(e.dataTransfer.files);
        return false;
    }
    function setImage(files) {
        [].forEach.call(files, function (file) {
            if(file.type.match('image/*')){
                var reader = new FileReader();
                reader.onload = function () {
                    var img = document.querySelector('div.image-wrapper img');
                    img.src = reader.result;
                    document.querySelector('input[name="Photo"]').value = reader.result;
                };
                reader.readAsDataURL(file);
            }
        })
    }
});