$('.editAccount').click(function () {
    $("#myModal .modal-content").load("/Manager/LoadAccount", { AccountID: $(this).attr('data-userid') });
});