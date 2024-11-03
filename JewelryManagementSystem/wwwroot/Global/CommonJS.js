/*Toster part successMessage and errorMessage*/
toastr.options = {
    "progressBar": true,
    "closeButton": true,
    "newestOnTop": true
}
function successMessage(message, status) {
    debugger;
    if (status == true) {
        toastr.success(message, '', { timeOut: 5000 });
    }
    else {
        toastr.error('An error occurred while processing the request please try again', '',{ timeOut: 10000 })
    }
}
function errorMessage(message, status) {
    if (status == false) {
        toastr.error(message, '', { timeOut: 5000 })
    }
    else {
        toastr.error('An error occurred while processing the request please try again', '', { timeOut: 10000 })
    }
}

/*Toster part successMessage and errorMessage*/

$(document).ajaxStart(function () {
    $("#loader-spinner").show();
}).ajaxStop(function () {
    $("#loader-spinner").hide();
});

function fnMarkup(template, data) {
    debugger;
    return Mustache.render(template, data);
}


