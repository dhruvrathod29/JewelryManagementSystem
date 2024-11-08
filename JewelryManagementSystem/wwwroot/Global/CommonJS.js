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
$(document).ajaxStart(function () {
    $("#loader-spinner").show();
}).ajaxStop(function () {
    $("#loader-spinner").hide();
});

function fnMarkup(template, data) {
    debugger;
    return Mustache.render(template, data);
}

function validateEmail(email) {
    // Regular expression for validating an email address
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(email);
}

function isValidNumber(input) {
    // Check if input is a string of 10 digits
    const regex = /^\d{10}$/;
    return regex.test(input);
}

function formatDate(dateString) {
    const date = new Date(dateString); // Convert string to Date object
    const months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    const day = String(date.getDate()).padStart(2, '0'); // Ensure 2 digits
    const month = months[date.getMonth()];
    const year = date.getFullYear();

    let hours = date.getHours();
    const minutes = String(date.getMinutes()).padStart(2, '0'); // Ensure 2 digits
    let ampm = "AM";

    if (hours >= 12) {
        ampm = "PM";
        if (hours > 12) hours -= 12; // Convert 24-hour to 12-hour format
    }
    if (hours === 0) hours = 12; // Midnight case

    return `${day}-${month}-${year} ${hours}:${minutes} ${ampm}`;
}
