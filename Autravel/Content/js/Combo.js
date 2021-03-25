$("#btnAddLocation").click(function () {
    var nameCategory = $('#post_Locationnew').val();
    $.ajax({
        type: 'POST',
        url: '/Manager/AddLocation/',
        data: { LocationName: nameCategory },
        success: function (result) {
            if (result != "") {
                $(".box.box-info.box.Category.Location ul#Locationchecklist").append("<li id=" + result + "><label class='selectit'><input value=" + result + " type='radio' name='tour_Location' id=" + result + "> " + nameCategory + "</label></li>");
                $('#post_Locationnew').val('');
            } else { alert("Có lỗi xảy ra trong quá trình lưu dữ liệu, Vui lòng thử lại!") }
        }
    });
});
$("#btnAddTopic").click(function () {
    var TopicTourNames = $('#post_Topicnew').val();
    $.ajax({
        type: 'POST',
        url: '/Manager/AddTopicTour/',
        data: { TopicTourName: TopicTourNames },
        success: function (result) {
            if (result != "") {
                $(".box.box-info.box.TopicTour.Category ul#TopicTourchecklist").append("<li id=" + result + "><label class='selectit'><input value=" + result + " type='checkbox' name='tour_Topic' id=" + result + "> " + TopicTourNames + "</label></li>");
                $('#post_Topicnew').val('');
            } else { alert("Có lỗi xảy ra trong quá trình lưu dữ liệu, Vui lòng thử lại!") }
        }
    });
});

//Star Rating
$(".RatingStar .fa").click(function () {
    var id = $(this).val();
    var ValueRate = 0;
    for (var x = 1; x < id; x++) {
        var elm = "#Star" + x;
        $(elm).removeClass("fa-star-o");
        $(elm).removeClass("fa-star-half-full");
        $(elm).removeClass("checked");
        $(elm).addClass("fa-star checked");
    }
    for (var x = id+1; x <= 5; x++) {
        var elm = "#Star" + x;
        $(elm).removeClass("fa-star");
        $(elm).removeClass("fa-star-half-full");
        $(elm).removeClass("checked");
        $(elm).addClass("fa-star-o");
    }
    if ($(this).hasClass("fa-star-half-full")) {
        $(this).removeClass("fa-star-half-full");
        $(this).removeClass("fa-star-o");
        $(this).addClass("fa-star");
        ValueRate = id;
    } else { $(this).addClass("fa-star-half-full checked"); ValueRate = id - 0.5; }
    console.log(ValueRate);
    $("#Star_Rating").val(ValueRate);
});
//Add RoomHotel
$("#Add_RoomHotel").click(function () {
    $(".Hotel_AllRoom .box-body").append("<div class='itemRoom col-md-12'data-roomhotelid='0'><div class='ListImgRoom col-md-3'><img src=''class='img_roomhotel'/><a href='#' class='thickbox btn selectmedia selectImgRoom'data-toggle='modal' data-target='#myModal'>Chọn ảnh</a><div class='form-group'><div class='input-group div_RoomHotel_Acreage'><span class='input-group-addon label_input'>Diện tích</span><input type='text'class='form-control RoomHotel_Acreage'name='RoomHotel_Acreage'value =''/></div></div><div class='form-group'><div class='input-group div_RoomHotel_TotalPrice'><span class='input-group-addon label_input'>Giá tiền</span><input type='text'class='form-control RoomHotel_TotalPrice'name='RoomHotel_TotalPrice'value =''/></div></div></div><div class='ExtentionRoom col-md-5'><div class='form-group'><div class='input-group div_RoomName'><span class='input-group-addon label_input'>Tên phòng</span><input type='text'class='form-control RoomName'name ='RoomName'value =''/></div></div><div class='form-group'><div class='input-group div_RoomHotel_View'><span class='input-group-addon label_input'>Hướng</span> <input type='text'class='form-control RoomHotel_View'name ='RoomHotel_View'value =''/></div></div><div class='form-group'><div class='input-group div_RoomHotel_Bed'><span class='input-group-addon label_input'>Giường</span><input type='text'class='form-control RoomHotel_Bed'name ='RoomHotel_Bed'value =''/></div></div><div class='form-group'><div class='input-group div_People'><span class='label_input col-md-3'>Sốlượng</span><div class='form-group col-md-9'><div class='input-group col-md-6 div_RoomHotel_Adutls'><span class='input-group-addon label_input'>NL</span><input type='text'class='form-control RoomHotel_Adutls'name ='RoomHotel_Adutls'value =''/></div><div class='input-group col-md-6 div_RoomHotel_Infants'><span class='input-group-addon label_input'>TE</span><input type='text'class='form-control RoomHotel_Infants'name = 'RoomHotel_Infants'value =''/></div></div></div></div></div><div class='ContentRoom col-md-4'><div class='form-grouprow'><span class='control-label'>Thông tin phòng</span><textarea rows='10'class='infoContentRoom form-control'></textarea></div></div></div>");
});
