$(document).ready(function () {
    console.log("ready!");

    $('#GuestOrderTable').DataTable({
        processing: true,
        ordering: true,
        paging: true,
        searching: true,
        "pageLength": 25

    });

   


});