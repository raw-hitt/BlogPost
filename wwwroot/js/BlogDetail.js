
$(document).ready(function () {

    const urlParams = new URLSearchParams(window.location.search);
    const myParam = urlParams.get('bid');
    GetBlogById(myParam);

});



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

    var blog = $("<div class='myBlog'><div class='blog-list'><div class='blog-title'>" +
        value.title + "<div class='views'>Views : "+value.views+"</div> </div><div class='blog-content'>" + value.content + "</div><div class='blog-details'>" +
        "- Published By :" + value.author + "<br/>- Published On - " + value.publishDate+"</div></div></div>");

    $('#BlogPost').append(blog);


}

