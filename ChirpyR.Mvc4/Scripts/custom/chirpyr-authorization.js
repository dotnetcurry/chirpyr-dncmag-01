/// <reference path="../knockout-2.0.0.debug.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="../jquery-1.7.2.js" />
/// <reference path="../ajax-util.js" />
/// <reference path="../ko-protected-observable.js" />

$(function () {
    $("#registerPopupContainer").hide();
    $("#loginPopupContainer").hide();
    $("#manageAccountPopupContainer").hide();

    $(document).on("click", "#register", function () {
        //console.log($("#UserName").val());
        $("#registerPopupContainer").dialog({
            minWidth: 400,
            width: 500,
            maxWidth: 600,
            title: "ChirpyR Registration",
            buttons: [
                {
                    text: "Register",
                    click: function () {
                        var user = {
                            "UserId": $("#UserId").val(),
                            "Password": $("#Password").val(),
                            "Email": $("#Email").val()
                        };
                        ajaxAdd("/Api/Registration", ko.toJSON(user), function (data) {
                            window.location.href = "/";
                        });
                        $(this).dialog("close");
                    }
                },
                {
                    text: "No Thanks, I'll just watch!",
                    click: function () { $(this).dialog("close"); }
                }
            ]
        });

    });
    $(document).on("click", "#login", function () {
        $("#loginPopupContainer").dialog({
            minWidth: 400,
            width: 400,
            maxWidth: 600,
            title: "Login",
            buttons: [
        {
            text: "Login",
            click: function () {
                var user = {
                    "UserId": $("#LoginUser").val(),
                    "Password": $("#LoginPassword").val()
                }
                ajaxAdd("/Api/Login", ko.toJSON(user), function (data) {
                    window.location("//");
                });
                $(this).dialog("close");
            }
        },
        {
            text: "Cancel",
            click: function () { $(this).dialog("close"); }
        }]

        });
    });
    $(document).on("click", "#userIdLabel", function () {
        var user = {
            "UserId": $("#userIdLabel")[0].innerHTML
        }
        $.getJSON("/Api/Registration?UserId=" + $("#userIdLabel")[0].innerHTML, ko.toJSON(user), function (data) {
            var viewModel = {
                // data
                currentUser: new ko.protectedObservableItem(data)
            }; // end viewModel
            ko.applyBindings(viewModel);
            $("#manageAccountPopupContainer").dialog({
                minWidth: 400,
                width: 400,
                maxWidth: 600,
                title: "Manage Account",
                buttons: [
        {
            text: "Save",
            click: function () {
            viewModel.currentUser.commit();
                        ajaxUpdate("/Api/Registration/", ko.toJSON(viewModel.currentUser));
                $(this).dialog("close");
            }
        },
        {
            text: "Cancel",
            click: function () {
                $(this).dialog("close");
            }
        }
        ]

            });
        });

    });

    $(document).on("click", "#logout", function () {
        ajaxUpdate("/Api/Login", null, function (data) {
            window.location("//");
        });
    });
});
