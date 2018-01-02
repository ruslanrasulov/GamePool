'use strict';

(function () {
    var $previewImage = $('#preview-image'),
        $fileInput = $('#game-avatar');

    $fileInput.change(function () {
        readURL(this);
    });

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $previewImage.attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }
    }
})();