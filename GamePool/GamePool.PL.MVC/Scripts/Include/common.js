'use strict';

$(function () {
    var $buyButtons = $('.buy-btn'),
        $quantityInputs = $('.quantity-input'),
        $totalPrice = $('#order-total-price'),
        totalPrice = 0,
        $removeOrderedGameButtons = $('.remove-ordered-game');

    $quantityInputs.each(function () {
        var quantity = this.value,
            unitPrice = this.parentElement.nextElementSibling.textContent,
            totalPriceElement = this.parentElement.nextElementSibling.nextElementSibling,
            totalGamePrice;

        unitPrice = unitPrice.substring(0, unitPrice.length - 1);

        totalGamePrice = +quantity * +unitPrice;
        totalPriceElement.textContent = totalGamePrice + '$';
        totalPrice += totalGamePrice;
    });

    $totalPrice.text(totalPrice + '$');

    $buyButtons.on('click', function (e) {
        var gameId = e.target.getAttribute('data-game-id');

        $.get('/Order/AddGameToOrders', { gameId: gameId }, function (data) {
            if (data) {
                e.target.classList.remove('buy-btn');
                e.target.classList.remove('btn-default');
                e.target.classList.add('btn-success');
                e.target.innerText = 'Added';
            }
        });
    });

    $removeOrderedGameButtons.on('click', function (e) {
        var $targetElement = $(e.target),
            gameId = $targetElement.data('game-id');

        $.get('/Order/RemoveGameFromOrders', { gameId: gameId }, function (data) {
            var orderPrice,
                currTotalPrice;

            if (data) {
                orderPrice = $targetElement.parent().siblings('.ordered-game-total-price').text();
                console.log($totalPrice.text());
                console.log($targetElement.parents('tr'));
                $targetElement.parents('tr').remove();

                currTotalPrice = $totalPrice.text();

                orderPrice = orderPrice.substring(0, orderPrice.length - 1);
                console.log(orderPrice);
                $totalPrice.text(+currTotalPrice.substring(0, currTotalPrice.length - 1) - orderPrice + '$');
            }
        });
    });

    $quantityInputs.on('change', function (e) {
        var $target = $(e.target),
            $totalGamePrice = $target.parent().siblings('.ordered-game-total-price'),
            quantity = $target.val(),
            gameId = $target.data('game-id'),
            unitPrice = $target.parent().siblings('.ordered-game-price').text();

        unitPrice = unitPrice.substring(0, unitPrice.length - 1);

        $.get('/Order/UpdateGameQuantity', { gameId: gameId, quantity: quantity }, function (data) {
            var totalPrice = $totalPrice.text(),
                totalGamePrice = $totalGamePrice.text();

            totalPrice = +totalPrice.substring(0, totalPrice.length - 1);
            totalGamePrice = +totalGamePrice.substring(0, totalGamePrice.length - 1);

            if (data) {
                totalPrice -= totalGamePrice;
                totalGamePrice = +quantity * +unitPrice;
                totalPrice += totalGamePrice;

                $totalGamePrice.text(totalGamePrice + '$');
                $totalPrice.text(totalPrice + '$');
            }
        });
    });
});