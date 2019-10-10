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
                // сохраняем в хранилище sessionStorage токен доступа
                sessionStorage.setItem(tokenKey, data.access_token);
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
 