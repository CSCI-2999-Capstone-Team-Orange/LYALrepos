function onUpdateClick($div, $li) {

    var $editCard = $("#editCard").clone().insertAfter($div);
    
    drinkData = $div.data("prod");
    itemData = $li.data("item")

    $("img", $editCard).attr("src", `/Images/${itemData.drinkName}.jpg`);
    $("h5", $editCard).html(`${itemData.sizeName}`)
    $("[name='updateProduct.ProductId']", $editCard).val(itemData.productId);
    $("[name='updateProduct.DrinkId']", $editCard).val(itemData.drinkId);
    $("[name='updateProduct.DrinkName']", $editCard).val(itemData.drinkName);
    $("[name='updateProduct.DrinkDescription']", $editCard).val(itemData.drinkDescription);
    $("[name='updateProduct.SizeId']", $editCard).val(itemData.sizeId);
    $("[name='updateProduct.SizeName']", $editCard).val(itemData.sizeName);
    $("[name='updateProduct.Price']", $editCard).val(itemData.price);
    

    $div.hide();
    $editCard.show();
}

function onCancelClick($div) {

    $div.hide();
    $div.prev().show();
}

function onSelectHide($selected) {
    let $selectedValue = ($selected.val());
    var drinkName = [null, "Coffee", "Cappuccino", "Latte", "Espresso", "Heywood"];
    let $drinkDivId = drinkName[$selectedValue];

    

    if ($selectedValue !="") {
        $('.col-lg-6').hide();
        $("div[id*=" + $drinkDivId + "]").show();
    }
    else {
     $('.col-lg-6').show();
    }
    /*$("div[id*!=" + $drinkDivId + "]").show();*/
    //$('#'+$drinkDivId).show();
    //$('#'+$drinkDivId).siblings().hide();

    
    

}

//function onUpdateClick($row) {
//    // jquery ajax post with values from the form fields
//    rowData = {};
//    $.post("url", rowData);
//    // callback for post success
//    {
//        // reverse the editClick

//    }
//}



$(document).ready(function () {
    console.log("ready!");

    $(".updateButton").on("click",function () {
        onUpdateClick($(this).parents(".col-lg-6"), $(this).parent(".list-group-item"));
        
    });

    $("#cardDeck").on("click", ".cancelButton", function () {
        onCancelClick($(this).parents(".col-lg-6"));
        

    });

    $("select").change(function () {
        onSelectHide($(this));
    });


});