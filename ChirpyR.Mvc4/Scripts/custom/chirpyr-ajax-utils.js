/// <reference path="jquery-1.6.2.js" />

$(function () {
    $.ajaxSetup({ cache: false });
});

function ajaxAdd(url, dataToSave, callback) {
    ajaxModify(url, dataToSave, "POST", "Tag Added.", callback);
}

function ajaxUpdate(url, dataToSave, successCallback) {
    ajaxModify(url, dataToSave, "PUT", "Tag Updated.", successCallback);
}

function ajaxDelete(url) {
    ajaxModify(url, null, "DELETE", "Tag Deleted.");
}

function ajaxModify(url, dataToSave, httpVerb, successMessage, callback) {
    $.ajax(url, {
        data: dataToSave,
        type: httpVerb,
        dataType: 'json',
        contentType: 'application/json',
        success: function(data) {
//            $.notifyBar({
//                html: successMessage,
//                cls: "success",
//            });
            if (callback !== undefined) {
                callback(data);
            }
          },
        error: function (data) {
//            $.notifyBar({
//                html: "Unexpected error.",
//                cls: "error"
//            });
            console.log(arguments[1] + arguments[2]);
        }
    });
}
