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

function getCategoryData(Data) {
    $.ajax({
        url: routeURL + '/Controllers/api/Category/GetCategoryData/' + Data,
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

function CategoryModal(obj, isUpdate) {
    if (isUpdate != null) {
        $('#categoryName').val(obj.categoryName);
        $('#id').val(obj.id);
        $('#btnUpdate').removeClass("d-none");
        $('#btnSubmit').addClass("d-none");
    }
    else {
        $('#btnUpdate').addClass("d-none");
        $('#btnSubmit').removeClass("d-none");
        $('#id').val(0);
    }
    $('#categoryInput').modal("show");
};

function CloseCategoryModal() {
    $('#categoryForm')[0].reset();
    $('#id').val(0);
    $('#categoryName').val("");

    $('#categoryInput').modal("hide");
};

function SubmitCategoryModal() {
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
                    CloseCategoryModal();
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

function GetDelete(Data) {
    $.ajax({
        url: routeURL + '/Controllers/api/Category/GetCategoryData/' + Data,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1 && response.dataenum !== undefined) {
                DeleteCategoryModal(response.dataenum);
            }
        },
        error: function (xhr) {
            toastr.error("Error", "error");
        }
    });
}

function DeleteCategoryModal(obj) {
    if (obj !== null) {
        $('#categoryNameDelete').html(obj.categoryName);
        $('#idDelete').val(obj.id);

        $('#categoryDeleteInput').modal("show");
    }
}

function CloseDeleteModal() {
    $('#categoryDeleteForm')[0].reset();
    $('#idDelete').val(0);
    $('#categoryNameDelete').val("");

    $('#categoryDeleteInput').modal("hide");
}

function DeleteModalSubmit() {
    var id = parseInt($('#idDelete').val());
    $.ajax({
        url: routeURL + '/Controllers/api/Category/DeleteCategory/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1) {
                dataTable.ajax.reload();
                toastr.success(response.message, "success");
                CloseDeleteModal();
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