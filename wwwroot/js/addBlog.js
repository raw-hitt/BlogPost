﻿


$(document).ready(function () {
    $('.content').EasyEditor();
});



function CreateBlogAsync() {

    var Title = $("#title").val();
    var Content = $("#content").val();
    var PublishDate = $("#date").val();
    var ModifiedBy = 1;
    var CreatedBy = 1;
    var Author = "hello man";


    var _data = JSON.stringify({ "Author": Author,"Title": Title, "Content": Content, "PublishDate": PublishDate, "ModifiedBy": ModifiedBy, "CreatedBy": CreatedBy});
    console.log(_data);

    if (confirm('Are you sure to save ?')) {

        $.ajax({
            type: "POST",
            url: "/api/Blog/CreateBlogAsync",
            contentType: "application/json",
            dataType: "json",
            processData: true,
            data: _data,
            success: function (result, status, xhr) {

                if (result.data) {
                    alert('Blog Saved Successfully');
                    window.location.href = "/admin";

                }
                else {
                    alert(result.message);
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
                alert('There was an unexpected error');
            }
        });


    } else {
        alert('Operation Cancelled');
    }


}