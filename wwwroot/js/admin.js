function Logout() {

    localStorage.removeItem("userName");
    localStorage.removeItem("userId");
    localStorage.removeItem("lastLogin");
    window.location.href = "/admin/login";
}

$(document).ready(function () {

    var userName = localStorage.getItem("userName");
    var userId = localStorage.getItem("userId");
    var lastLogin = localStorage.getItem("lastLogin");

    if (!userName || !userId || !lastLogin) {
        window.location.href = "/admin/login";

    }
    var arr = userName.split('@');

    $("#username").text("Welcome " + arr[0]);

    console.log("Welcome " + userName);

});
