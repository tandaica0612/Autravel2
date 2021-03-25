$("#btnAddCategory").click(function () {
    var nameCategory = $('#post_categorynew').val();
    $.ajax({
        type: 'POST',
        url: '/Manager/AddCategory/',
        data: { NameCategory: nameCategory },
        success: function (result) {
            $(".box.box-info.box.Category ul#categorychecklist").append("<li id=" + result + "><label class='selectit'><input value=" + result + " type='checkbox' name='post_category' id=" + result + "> " + nameCategory +"</label></li>");
            $('#post_categorynew').val('');
        }
        });
});


$("#btnAddLocation").click(function () {
    var name = $('#post_Locationnew').val();
    $.ajax({
        type: 'POST',
        url: '/Manager/AddLocation/',
        data: { LocationName: name },
        success: function (result) {
            $(".box.box-info.box.Category ul#Locationchecklist").append("<li id=" + result + "><label class='selectit'><input value=" + result + " type='radio' name='tour_Location' id=" + result + "> " + name + "</label></li>");
            $('#post_Locationnew').val('');
        }
    });
});