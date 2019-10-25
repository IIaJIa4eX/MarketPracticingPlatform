// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var tokenKey = "accessToken";
        $('#submitLogin').click(function (e) {
            e.preventDefault();
            var loginData = {
                grant_type: 'password',
                username: $('#emailLogin').val(),
                password: $('#passwordLogin').val()
            };
 
            $.ajax({
                type: 'POST',
                url: '/Home/Token',
                data: loginData
            }).done(function (data) {
                $('.userName').text(data.username);
                $('.userInfo').css('display', 'block');
                $('.loginForm').css('display', 'none');
               // document.cookie = "username = " + data.username;
                console.log(data.access_token);
            }).fail(function (data) {
                console.log(data);
            });
        });
 
        $('#logOut').click(function (e) {
            e.preventDefault();
            $('.loginForm').css('display', 'block');
            $('.userInfo').css('display', 'none');
            sessionStorage.removeItem(tokenKey);
        });


$('#getDataByRole').click(function (e) {
    e.preventDefault();
    $.ajax({
        type: 'GET',
        url: '/api/values/getrole',
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (data) {
            alert(data);
        },
        fail: function (data) {
            console.log(data);
        }
    });
});

function SentToken() {
    var x = document.cookie;
    var token = x.Token;
    $.ajax({
        type: 'GET',
        url: '/MarketSearch/Index',
        beforeSend: function (xhr) {         
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        fail: function (data) {
            console.log(data);
        }
    });
}