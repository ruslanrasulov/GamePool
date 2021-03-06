﻿'use strict';

var CFG = CFG || {};

$(function () {
    var $pageLinks = $('.page-link'),
        currUrl;

    if (CFG && CFG.action) {
        currUrl = CFG.action;
    } else {
        currUrl = window.location.href;
    }

    $pageLinks.each(function () {
        var newUrl,
            pageNumber = this.getAttribute('data-page');

        if (currUrl.search(/PageNumber=\d*/i) === -1) {
            newUrl = currUrl + '?PageNumber=' + pageNumber;
        } else {
            newUrl = currUrl.replace(/PageNumber=\d*/i, 'PageNumber=' + pageNumber);
            newUrl = newUrl.replace(/&amp;/g, '&');
        }

        this.setAttribute('href', newUrl);
    });
});