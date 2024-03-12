


$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('bid');
    GetBlogById(myParam);

    $('.content').EasyEditor();
});



function UpdateBlog() {

    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('bid');

    var userName = localStorage.getItem("userName");
    var userId = localStorage.getItem("userId");

    var Title = $("#title").val();
    var Content = $("#content").val();
    var ModifiedBy = userId;
    var Author = userName;


    var _data = JSON.stringify({ "Id": myParam, "Author": Author, "Title": Title, "Content": Content, "ModifiedBy": ModifiedBy });
    console.log(_data);

    if (confirm('Are you sure to save ?')) {

        $.ajax({
            type: "POST",
            url: "/api/Blog/UpdateBlog",
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

function GetBlogById(id) {

    $.ajax({
        type: "GET",
        url: "/api/Blog/GetBlogByIdAndUpdateView/" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result, status, xhr) {

            console.log(result);
            injectBlogDetail(result.data);
        },
        error: function (xhr, status, error) {
            console.log(error);
            alert('There was an unexpected error');
        }
    });
}

function injectBlogDetail(value) {

    console.log(value);
    $("#title").val(value.title);
    $("#content").val(value.content);

}