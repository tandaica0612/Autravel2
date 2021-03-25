
$('.selectmedia').click(function () {
    $("#AreaSelectImage").val(this.id);
    var AreaSelectImage = $("#AreaSelectImage").val();
    var l_OldItemIMG = "";
    if (AreaSelectImage == "set-post-thumbnail") {
        l_OldItemIMG = $("#_thumbnail_id").val();
    } else if (AreaSelectImage == "selectmultimedia") {
        l_OldItemIMG = $('#List_Images_Combo').val(); }
    $("#myModal .modal-body").load("/Manager/ptLoadAllImage", function () {
        if (l_OldItemIMG != "") {
            l_OldItem = l_OldItemIMG.split(',');
            multyimg = $('.imageitem');
            for (var y = 0; y < l_OldItem.length; y++) {
                for (var x = 0; x < multyimg.length; x++) {
                    if (multyimg[x].getAttribute('data-srcitem') === l_OldItem[y]) { multyimg[x].attributes.class.value = "col-md-2 imageitem selected"; break; }
                }
            }
        }
    });
}); 
$('.imageitem').click(function () {
    if ($(this).hasClass("selected")) { $(this).removeClass("selected"); }
    else { if ($("#AreaSelectImage").val() != "selectmultimedia") { $('.imageitem').removeClass("selected"); } $(this).addClass("selected");}
}); 
$('#submitselectimage').click(function () {
    img = $('.imageitem.selected img')[0].outerHTML;
    if ($("#AreaSelectImage").val() == "insert-media-button") {
        CKEDITOR.instances.editor.insertHtml(img);
    } else if ($("#AreaSelectImage").val() == "set-post-thumbnail") {
        $(".postimagediv #category-all .inside img").attr('src', $('.imageitem.selected img').attr('src'));
        $("#_thumbnail_id").val($('.imageitem.selected img').attr('src'));
    } else if ($("#AreaSelectImage").val() == "selectmultimedia") {
        multyimg = $('.imageitem.selected img');
        $("#List_Images_Combo").val('');
        $(".Select_multi_Media .item_image").remove()
        for (var x = 0; x < multyimg.length; x++) {
            $(".form-group.div_List_Images.col-md-12").append("<div class='form-group col-md-2 item_image'><img src = '" + multyimg[x].attributes[0].value + "' class= 'item_image_combo'></div>")
            $("#List_Images_Combo").val($("#List_Images_Combo").val()+ multyimg[x].attributes[0].value + ",");
        }
    }
    $('.imageitem').removeClass("selected"); 
    $('.modal-backdrop.fade.in').remove();
    $('#myModal').modal('toggle');
});
$('#delete-post-thumbnail').click(function () {
    $(".postimagediv #category-all .inside img").attr('src', '');
    $("#_thumbnail_id").val('');
});
    function updatemark(arg)
    {  
        var abc = ""
        //Iterating the collection of checkboxes which checked marked
        $('input[name="post_category"]').each(function () {  
            if (this.checked) {
            abc = abc + $(this).val() + ","
            //assign set value to hidden field   
            $('#post_category_arr').val(abc);
        }
        });
}
function Tour_FixedonClickHandler(ev) {
    if (ev.checked) {
        $(".Price_Tour_UnFixed").css("display", "none");
        $(".Price_Tour_Fixed").css("display", "block");
    } else {
        $(".Price_Tour_Fixed").css("display", "none");
        $(".Price_Tour_UnFixed").css("display", "block");
    }
}
