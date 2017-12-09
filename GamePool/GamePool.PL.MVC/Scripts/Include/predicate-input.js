"use strict";

﻿$(function () {
    $(".genres-input").select2({
        minimumInputLength: 1,
        placeholder: "Select a genres",
        ajax: {
            url: "/Admin/GetGenresByNamePart",
            dataType: "json",
            data: function (params) {
                return {
                    name: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data
                };
            }
        }
    });
});