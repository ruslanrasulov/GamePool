'use strict';

var CFG = CFG || {};

﻿$(function () {
     var $genresInput = $('.genres-input');

     if (CFG && CFG.genreIds && CFG.genreIds.length !== 0) {
         $.post(
             '/Admin/GetGenresByIds',
             $.param({ ids: CFG.genreIds }, true),
             function (data) {
                 renderOptions(data);
             });
     }

     $genresInput.select2({
         minimumInputLength: 1,
         placeholder: 'Select a genres',
         ajax: {
             url: '/Admin/GetGenresByNamePart',
             dataType: 'json',
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

     function renderOptions(data) {
         var obj = JSON.parse(data),
             i;

         for (i = 0; i < obj.genres.length; i++) {
             $genresInput.append($('<option>')
                 .attr('value', obj.genres[i].id)
                 .attr('selected', 'selected')
                 .text(obj.genres[i].text));
         }
     }
});