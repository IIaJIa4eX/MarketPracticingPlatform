// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function SubmitAuthentication() {

    $.post("/Home/UserAuthentication", $("#PostForm").serialize(), function (data) {
        if (data.isSuccess === true) {

            document.location.href = "/Home";

        } else {

            document.getElementById("ErrorDisplay").innerHTML = data.errorMessage;
        }
        
    }).fail(function (data) {

        if (data.status === 500) {
            document.getElementById("ErrorDisplay").innerHTML = "Не удалось подключиться к серверу";
        }
    });

}

function SubmitRegistration() {

    $.post("/Registration/UserCreation", $("#RegistrationForm").serialize(), function (data) {
        if (data.isSuccess === true) {

            document.location.href = "/Home";

        } else {

            document.getElementById("RegistrationErrorView").innerHTML = data.errorMessage;
        }

    }).fail(function (data) {

        if (data.status === 500) {
            document.getElementById("RegistrationErrorView").innerHTML = "Не удалось подключиться к серверу";
        }
    });

}

function SubmitCategoryCreation() {

    $.post("/Market/CategoryCreation", $("#CategoryCreationForm").serialize(), function (data) {
        if (data.isSuccess === true) {

            document.location.href = "/Market/Index";

        } else {

            document.getElementById("CategoryCreationErrorView").innerHTML = data.errorMessage;
        }

    }).fail(function (data) {

        if (data.status === 500) {
            document.getElementById("CategoryCreationErrorView").innerHTML = "Не удалось подключиться к серверу";
        }
    });

}

function SubmitProductCreation() {

    $.post("/Market/ProductCreation", $("#ProductCreationForm").serialize(), function (data) {
        if (data.isSuccess === true) {

            document.location.href = "/Market/Index";

        } else {

            document.getElementById("ProductCreationErrorView").innerHTML = data.errorMessage;
        }

    }).fail(function (data) {

        if (data.status === 500) {
            document.getElementById("ProductCreationErrorView").innerHTML = "Не удалось подключиться к серверу";
        }
    });

}

$("input").click(function () {
    $(this).parent().children("small").slideDown();
}).blur(function () {
    $(this).parent().children("small").slideUp();
});




