function addToCart(prodId) {
    var productId = "productId_" + prodId;
    var product = document.getElementById(productId);

    $.ajax({
        type: "POST",
        url: "/Purchase/AddToShoppingCart",
        data: { id: product.value },
        success: function () {
            $('#shopping-cart')
                .load('/Purchase/CountOfProductsInCart',
                    function () {
                        $(this).children(':first').unwrap();
                        window.location.href = '/Products/All/#' + prodId;
                    });
        }
    });
}

function loadProductFilters(selectObject) {
    if (selectObject.value == 'hardDrive') {
        $('#product-search-form').attr("action", "/Products/FilterHardDrives");
    }
    if (selectObject.value == 'graphicCard') {
        $('#product-search-form').attr("action", "/Products/FilterGraphicCards");
    }
    if (selectObject.value == 'processor') {
        $('#product-search-form').attr("action", "/Products/FilterProcessors");
    }
    if (selectObject.value == 'none') {
        $('#product-search-form').attr("action", "/Products/FilterProducts");
    }
    $('#particular-product-filter').load('/Admin/Control/ProductSpecs', { productType: selectObject.value });
}

$(function () {
    $("#slider-range").slider({
        range: true,
        min: 30,
        max: 1000,
        values: [75, 700],
        slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount").val("$" + $("#slider-range").slider("values", 0) +
      " - $" + $("#slider-range").slider("values", 1));
});
