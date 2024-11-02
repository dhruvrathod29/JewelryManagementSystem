$(document).ready(function () {
    debugger;
    FillData();
});

function FillData() {
    debugger;

    $.ajax({
        type: "POST",
        url: "/CategoryMst/CategoryMst/FillData",
        success: function (result) {
            if (result != null) {
                debugger;
            }
        },
        error: function (req, status, message) {
            errorMessage(message, status)
        }

    });
}