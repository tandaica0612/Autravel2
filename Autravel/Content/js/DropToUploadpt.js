//Drop to upload media to server on ptLoadAllImage
$(function () {
    $('#dropArea').filedrop({
        url: '/Manager/UploadFiles/',
        allowedfiletypes: ['image/jpeg', 'image/png', 'image/gif'],
        allowedfileextensions: ['.jpg', '.jpeg', '.png', '.gif'],
        paramname: 'files',
        maxfiles: 5,
        maxfilesize: 5, // in MB
        dragOver: function () {
            $('#dropArea').addClass('active-drop');
        },
        dragLeave: function () {
            $('#dropArea').removeClass('active-drop');
        },
        drop: function () {
            $('#dropArea').removeClass('active-drop');
        },
        afterAll: function (e) {
            $('#dropArea').html('file(s) uploaded successfully');
        },
        uploadFinished: function (i, file, response, time) {
            //window.location.reload()
            var AreaSelectImage = $("#AreaSelectImage").val();
            var l_OldItemIMG = "";
            if (AreaSelectImage == "set-post-thumbnail") {
                l_OldItemIMG = $("#_thumbnail_id").val();
            } else if (AreaSelectImage == "selectmultimedia") {
                l_OldItemIMG = $('#List_Images_Combo').val();
            }
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
        }
    })
})