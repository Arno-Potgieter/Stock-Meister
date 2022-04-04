var routeURL = location.protocol + "//" + location.host;

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
}

function onShowCategoryModal(obj, ) {
    $('#categoryInput').modal("show");
}

function onCloseCategoryModal() {
    $('#categoryForm')[0].reset();
    $('#id').val(0);
    $('#categoryName').val("");

    $('#categoryInput').modal("hide");
}

function onSubmitCategoryModal() {
    if (checkCategoryValidation()) {
        var requestData = {
            Id: parseInt($('#id').val()),
            Name: $('#categoryName').val(),
        };

        $.ajax({
            url: routeURL + 'api/Category/SaveCategoryData',
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    toastr.success(response.message);
                    onCloseCategoryModal();
                }
                else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                toastr.error(response.message);
            }
        });
    }
}