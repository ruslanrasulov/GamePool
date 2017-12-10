"use strict";

$(function () {
    var $pageLinks = $(".page-link"),
        currUrl = CFG.action || window.location.href;

    $pageLinks.each(function () {
        var newUrl,
            pageNumber = this.getAttribute("data-page");

        if (currUrl.search(/PageNumber=\d*/i) === -1) {
            newUrl = currUrl + "?PageNumber=" + pageNumber;
        } else {
            newUrl = currUrl.replace(/PageNumber=\d*/i, "PageNumber=" + pageNumber);
            newUrl = newUrl.replace(/&amp;/g, "&");
        }
        console.log(pageNumber);
        this.setAttribute("href", newUrl);
    });
});