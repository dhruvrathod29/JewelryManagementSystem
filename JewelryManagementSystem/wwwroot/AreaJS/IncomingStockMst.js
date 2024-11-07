$(document).ready(function () {
    debugger;
    FillIncomingStock();
});

function FillIncomingStock() {

}

function btnNewIncomingStock() {
    $('#IncomingStock_modal').modal('show');
}

function ddlFillProduct() {

    debugger;
    if ($("#ddlIncomingCategory").val() != "") {
        $("#ddlIncomingProduct").empty();
        $("#ddlIncomingProduct").append($("<option></option>").val("").html("--Select Product--"));

        formData = new FormData();
        formData.append('p_sId', $("#ddlIncomingCategory").val());

        $.ajax({
            type: "POST",
            url: "/IncomingStockMst/IncomingStockMst/ddlFillProduct",
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                if (result != null) {
                    $.each(result,
                        function (key, value) {
                            $("#ddlIncomingProduct").append($("<option></option>").val(value.value).html(value.text));
                        });
                }
            },
            error: function (req, status, message) {
                errorMessage(message, status)
            }
        });
    }
    else {
        $("#ddlIncomingProduct").empty();
        $("#ddlIncomingProduct").append($("<option></option>").val("").html("--Select Product--"));
    }
}