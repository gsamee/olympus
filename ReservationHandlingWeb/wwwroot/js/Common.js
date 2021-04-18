$(document).ready(function () {
    $('#DataTable').DataTable({
        "columnDefs": [
            { "orderable": false, "targets": "no-sort" }
        ]
    });
    $('#DataTable_wrapper').css("font-size", "12px");
});