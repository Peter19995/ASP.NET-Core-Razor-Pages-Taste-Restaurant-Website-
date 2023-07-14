var dataTable;
$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("cancelled")) {
        loadList("cancelled");
    } else {

        if (url.includes("ready")) {
            loadList("ready");
        } else {

            if (url.includes("completed")) {
                loadList("completed");
            } else {
                loadList("inProcess");
            }
        }
    }
}); 

function loadList(param) {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/order?status=" + param,
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "pickUpName", "width": "20%" },
            { "data": "user.email", "width": "20%" },
            { "data": "orderTotal", "width": "20%" },
            { "data": "pickUpTime", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-75 btn-group"><a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2"><i class="bi bi-pencil-square"></i> </a></div>`
                },
                "width": "25%"
            }

        ],
        "autoWidth": true
    });
}