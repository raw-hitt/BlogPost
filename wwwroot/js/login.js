

function Login() {

    var UserName = $("#UserName").val();
    var Password = $("#Password").val();

    var _data = JSON.stringify({ "Password": Password, "UserName": UserName });
    console.log(_data);

    $.ajax({
        type: "POST",
        url: "/api/User/Login",
        contentType: "application/json",
        dataType: "json",
        processData: true,
        data: _data,
        success: function (result, status, xhr) {

            console.log(result);

            if (result.statusCode==200) {

                localStorage.setItem("userName", result.data.userName);
                localStorage.setItem("userId", result.data.userId);
                localStorage.setItem("lastLogin", result.data.lastLogin);

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





}