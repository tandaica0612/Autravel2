$("a[href='" + location.pathname + "']").addClass('active');
window.alert = function (message) {

    if (message == "OK") {
        notify(message);
        return;
    }
    Swal.fire({
        title: message,
        allowOutsideClick: false
    })
};


window.notify = function (message) {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: message,
        showConfirmButton: false,
        timer: 1500
    })
};




// Ajax Loading
$(document).ajaxStart(function () {
    $('#loading').fadeIn('fast');
    $('button').attr("disabled", true);
    $('a').addClass("disabled");
    // Disable buttons
});
$(document).ajaxStop(function () {
    $('#loading').fadeOut('fast');
    $('button').attr("disabled", false);
    $('a').removeClass("disabled");
    // Enable buttons
});