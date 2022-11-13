var routeURL = location.protocol + "//" + location.host;
var dataTable;

$(document).ready(function () {
    loadProductTable();
});

function loadProductTable() {
    dataTable = $('#productTable').DataTable({
        "ajax": {
            "url": "/Controllers/api/Product/GetAllProducts"
        },
        "columns": [
            { "data": "productName" }, 
            { "data": "productPrice" },
            {"data": "category.categoryName"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="w-100 text-center" role="group">
                                <i class="fa-solid fa-pen-to-square click-hover me-2" onClick=getProductData(${data})></i>
                                <i class="fa-solid fa-trash click-hover" onClick=GetDelete(${data})></i>
                            </div>
                          `
                },
                "width": "10%"
            },
        ]
    });
};

function checkProductValidation() {
    var isValid = true;

    if ($("#productName").val() === undefined || $("#productName").val() === "") {
        isValid = false;
        $("#productName").addClass('error');
    }
    else {
        $("#productName").removeClass('error');
    }
    if ($("#productPrice").val() === undefined || $("#productPrice").val() === "") {
        isValid = false;
        $("#productPrice").addClass('error');
    }
    else {
        $("#productPrice").removeClass('error');
    }
    if ($("#productCategory").val() === null) {
        isValid = false;
        $("#productCategory").addClass('error');
    }
    else {
        $("#productCategory").removeClass('error');
    }

    return isValid;
};

function getProductData(Data) {
    $.ajax({
        url: routeURL + '/Controllers/api/Product/GetProductData/' + Data,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1 && response.dataenum !== undefined) {
                ProductModal(response.dataenum, 1);
            }
        },
        error: function (xhr) {
            toastr.error("Error", "error");
        }
    });
}

function ProductModal(obj, isUpdate) {
    if (isUpdate != null) {
        $('#productName').val(obj.productName);
        $('#productPrice').val(obj.productPrice);
        $('#productCategory').val(obj.productCategory);
        $('#id').val(obj.id);
        $('#btnUpdate').removeClass("d-none");
        $('#btnSubmit').addClass("d-none");
    }
    else {
        $('#btnUpdate').addClass("d-none");
        $('#btnSubmit').removeClass("d-none");
        $('#id').val(0);
    }
    $('#productInput').modal("show");
};

function CloseProductModal() {
    $('#productForm')[0].reset();
    $('#id').val(0);
    $('#productName').val("");
    $('#productPrice').val("");

    $('#productInput').modal("hide");
};

function SubmitProductModal() {
    if (checkProductValidation()) {
        var requestData = {
            Id: parseInt($('#id').val()),
            ProductName: $('#productName').val(),
            ProductPrice: $('#productPrice').val(),
            CategoryId: $('#productCategory').val()
        };

        $.ajax({
            url: routeURL + '/Controllers/api/Product/SaveProductData',
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    dataTable.ajax.reload();
                    toastr.success(response.message, "success");
                    CloseProductModal();
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
};

function GetDelete(Data) {
    $.ajax({
        url: routeURL + '/Controllers/api/Product/GetProductData/' + Data,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {
            if (response.status === 1 && response.dataenum !== undefined) {
                DeleteProductModal(response.dataenum);
            }
        },
        error: function (xhr) {
            toastr.error("Error", "error");
        }
    });
};

function DeleteProductModal(obj) {
    if (obj !== null) {
        $('#productNameDelete').html(obj.productName);
        $('#idDelete').val(obj.id);

        $('#productDeleteInput').modal("show");
    }
};

function CloseDeleteModal() {
    $('#productDeleteForm')[0].reset();
    $('#idDelete').val(0);
    $('#productNameDelete').val("");

    $('#productDeleteInput').modal("hide");
};

function DeleteModalSubmit() {
    var id = parseInt($('#idDelete').val());
    $.ajax({
        url: routeURL + '/Controllers/api/Product/DeleteProduct/' + id,
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
};