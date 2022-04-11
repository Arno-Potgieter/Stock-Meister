var routeURL = location.protocol + "//" + location.host;
var dataTable

$(document).ready(function () {
    loadCategoryTable();
});

function loadCategoryTable() {
    dataTable = $('#categoryTable').DataTable({
        "ajax": {
            "url": "/Controllers/api/Category/GetAllCategories"
        },
        "columns": [
            { "data": "categoryName" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-100 text-center">
                                <a class="btn btn-primary mx-2" onClick=Delete('/Controllers/api/Category/${data}><i class="bi bi-pen"></i>View</a>
                            </div>
                          `
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-100" role="group">
                                <a class="btn btn-outline-dark mx-2" ><i class="bi bi-pen"></i>Edit</a>
                                <a class="btn btn-outline-danger mx-2" onClick=DeleteModal(${data})><i class="bi bi-trash"></i> Delete</a>
                            </div>
                          `            
                },
                "width": "15%"
            },
        ]
    });
};

function checkCategoryValidation() {
    var isValid = true;

    if ($("#categoryName").val() === undefined || $("#categoryName").val() === "") {
        isValid = false;
        $("#categoryName").addClass('error');
    }
    else {
        $("#categoryName").removeClass('error');
    }
    return isValid;
};

function onShowCategoryModal() {
    $('#id').val(0);
    $('#categoryInput').modal("show");
};

function onCloseCategoryModal() {
    $('#categoryForm')[0].reset();
    $('#id').val(0);
    $('#categoryName').val("");

    $('#categoryInput').modal("hide");
};

function onSubmitCategoryModal() {
    if (checkCategoryValidation()) {
        var requestData = {
            Id: parseInt($('#id').val()),
            CategoryName: $('#categoryName').val(),
        };

        $.ajax({
            url: routeURL + '/Controllers/api/Category/SaveCategoryData',
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    dataTable.ajax.reload();
                    toastr.success(response.message, "success");
                    onCloseCategoryModal();
                }
                else {
                    toastr.error(response.message, "error");
                }
            },
            error: function (xhr) {
                toastr.error("Error", "error");
            }
        });
    }
}

function DeleteModal(obj) {
    
}