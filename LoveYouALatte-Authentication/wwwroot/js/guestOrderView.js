$(document).ready(function () {
    console.log("ready!");

    $('#productTable').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        "pageLength": 25

    });

   
    });


});