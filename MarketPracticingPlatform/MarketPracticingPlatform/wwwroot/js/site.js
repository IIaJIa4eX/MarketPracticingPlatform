﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

        if (data.status === 401) {
            document.getElementById("CategoryCreationErrorView").innerHTML = "Вы не авторизованы";
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

        if (data.status === 401) {
            document.getElementById("ProductCreationErrorView").innerHTML = "Вы не авторизованы";
        }
    });

}


function SubmitProductEdit() {

    $.post("/Market/EditProduct", $("#ProductEditingForm").serialize(), function (data) {
        if (data.isSuccess === true) {

            document.location.href = "/Market/Index";

        } else {

            document.getElementById("EditErrorView").innerHTML = data.errorMessage;
        }

    }).fail(function (data) {

        if (data.status === 500) {
            document.getElementById("EditErrorView").innerHTML = "Не удалось подключиться к серверу";
        }

        if (data.status === 500) {
            document.getElementById("EditErrorView").innerHTML = "Вы не авторизованы";
        }
    });

}

$("input").click(function () {
    $(this).parent().children("small").slideDown();
}).blur(function () {
    $(this).parent().children("small").slideUp();
    });


$(function () {

    $('#CategoriesTree').jstree({
    "core": {
        "animation": 0,
        "data": {
            "url": function (node) {
                return "/Treeview/GetTreeNodes/";
            },
            "data": function (node) {
                // Each time jstree needs to make an AJAX call this function will be called.
                // It adds 'key' and 'isRoot' as parameter to ajax call. See signature of 'GetChildren' method.
                // # is the special ID that the function receives when jstree needs to load the root nodes.
                var isRoot = false;
                var key = node.id;
                if (key === "#") {
                    isRoot = true;
                    key = $("#CategoriesTree").data("key");
                }
                return { "key": key, "isRoot": isRoot };
            }
        }
    },
    "plugins": ["wholerow"]
    });
});

$('#CategoriesTree').on('changed.jstree', function (e, data) {

    var form = document.createElement("form");
    var element1 = document.createElement("input");
    form.method = "POST";
    form.style.visibility = "hidden";

    element1.value = data.node.id;
    element1.name = "CategoryId";
    form.appendChild(element1);
    
    document.body.appendChild(form);

    form.submit();

});




