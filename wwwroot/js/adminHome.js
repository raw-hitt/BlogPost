
$(document).ready(function () {
    getAllBlogs();

});



function getAllBlogs() {

    $.ajax({
        type: "GET",
        url: "/api/Blog/GetAllBlogs",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result, status, xhr) {

            console.log(result);
            injectBlogInList(result.data);
        },
        error: function (xhr, status, error) {
            console.log(error);
            alert('There was an unexpected error');
        }
    });
}



function injectBlogInList(data) {

    console.log(data);

    $.each(data, function (index, value) {
        var blog = $("<li class='myBlog'><div class='blog-list'><div class='blog-title'>" +
            value.title + "</div><div class='blog-content'>" + value.content + "</div><div class='blog-readMore'><a class='btnRead'  target='_blank' href='admin/EditBlog?bid=" + value.id + "'>Edit</a></div><div class='blog-readMore'><button class='btnRead btn-delete' onclick='DeleteBlog(" + value.id + ")'>Delete</button></div><div class='blog-details'>" +
            "- Published By :" + value.author + "</div></div></li>");
        $('#BlogList').append(blog);

    });
}

function DeleteBlog(id) {

    var _data = JSON.stringify({ "BlogId": id });
    console.log(_data);

    if (confirm('Are you sure to delete ?')) {

        $.ajax({
            type: "POST",
            url: "/api/Blog/DeleteBlog",
            contentType: "application/json",
            dataType: "json",
            processData: true,
            data: _data,
            success: function (result, status, xhr) {

                if (result.data) {
                    alert('Blog Deleted');
                    $('#BlogList').empty()
                    getAllBlogs();
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
