'use strict';

(function () {
    var $loginForm = $('#login-form'),
        $loginFormLink = $('#login-form-link'),
        $registrationForm = $('#registration-form'),
        $registrationFormLink = $('#registration-form-link'),
        fadeTime = 100;

    $loginFormLink.on('click', function () {
        swap($registrationForm, $loginForm);

        $registrationFormLink.removeClass('active');
        $loginFormLink.addClass('active');
    });

    $registrationFormLink.on('click', function () {
        swap($loginForm, $registrationForm);

        $registrationFormLink.addClass('active');
        $loginFormLink.removeClass('active');
    });

    function swap($elementOne, $elementTwo) {
        $elementOne.fadeOut(fadeTime);
        $elementOne.addClass('hidden');

        $elementTwo.fadeIn(fadeTime);
        $elementTwo.removeClass('hidden');
    }
})()