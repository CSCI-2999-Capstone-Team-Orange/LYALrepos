function onUpdateClick($tr) {

    var $editRow = $("#editRow").clone().insertAfter($tr);
    
    rowData = $tr.data("product");

    $("[name='updateProduct.ProductId']", $editRow).val(rowData.productId);
    $("[name='updateProduct.DrinkId']", $editRow).val(rowData.drinkId);
    $("[name='updateProduct.DrinkName']", $editRow).val(rowData.drinkName);
    $("[name='updateProduct.DrinkDescription']", $editRow).val(rowData.drinkDescription);
    $("[name='updateProduct.SizeId']", $editRow).val(rowData.sizeId);
    $("[name='updateProduct.SizeName']", $editRow).val(rowData.sizeName);
    $("[name='updateProduct.Price']", $editRow).val(rowData.price);
   

    
    $tr.hide();
    $editRow.show();
}

function onCancelClick($tr) {

    $tr.prev().show();
    $tr.hide();
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

    $('#productTable').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        "pageLength": 20
        
    });

    $(".updateButton").click(function () {
        onUpdateClick($(this).parents("tr"));
    });

    $("#menuTable").on("click",".cancelButton", function () {
        onCancelClick($(this).parents("tr"));
    });


});