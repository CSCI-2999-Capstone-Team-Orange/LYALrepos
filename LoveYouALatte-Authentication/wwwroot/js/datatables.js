function onUpdateClick($tr) {

    var $editRow = $("#editRow").clone().insertAfter($tr);
    
    rowData = $tr.data("product");

    $("[name='updateProduct.ProductId']", $editRow).val(rowData.productId);
    $("[name='updateProduct.ProductSku']", $editRow).val(rowData.productSku);
    $("[name='updateProduct.category']", $editRow).val(rowData.category);
    $("[name='updateProduct.DrinkId']", $editRow).val(rowData.drinkId);
    $("[name='updateProduct.DrinkName']", $editRow).val(rowData.drinkName);
    $("[name='updateProduct.DrinkDescription']", $editRow).val(rowData.drinkDescription);
    $("[name='updateProduct.SizeId']", $editRow).val(rowData.sizeId);
    $("[name='updateProduct.SizeName']", $editRow).val(rowData.sizeName);
    $("[name='updateProduct.Price']", $editRow).val(parseFloat(rowData.price).toFixed(2));
   

    $("#menuTable").find(".updateButton").hide();
    $tr.hide();
    $editRow.show();
}

function onCancelClick($tr) {

    $tr.prev().show();
    $tr.empty();
    $(".updateButton").show();
}





$(document).ready(function () {
    console.log("ready!");

    $('#productTable').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        "pageLength": 25
        
    });

    


    $("#menuTable").on("click",".updateButton", function () {
        onUpdateClick($(this).parents("tr"));
    });

    $("#menuTable").on("click",".cancelButton", function () {
        onCancelClick($(this).parents("tr"));
    });


});