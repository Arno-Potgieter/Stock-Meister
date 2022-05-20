var routeURL = location.protocol + "//" + location.host;
var dataTable

$(document).ready(function () {
    loadCategoryTable();
});

function loadProductTable() {
    dataTable = $('#productTable').DataTable({
        "ajax": {
            "url": "/Controllers/api/Product/GetAllProducts"
        },
        "columns": [
            { "data": "productName" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-100 text-center" role="group">
                                <i class="fa-solid fa-pen-to-square click-hover me-2" onClick=getCategoryData(${data})></i>
                                <i class="fa-solid fa-trash click-hover" onClick=GetDelete(${data})></i>
                            </div>
                          `
                },
                "width": "10%"
            },
        ]
    });
};

function getProductData(Data) {
    $.ajax({
        url: routeURL + '/Controllers/api/Product/GeProductData/' + Data,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1 && response.dataenum !== undefined) {
                CategoryModal(response.dataenum, 1);
            }
        },
        error: function (xhr) {
            toastr.error("Error", "error");
        }
    });
}