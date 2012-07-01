/// <reference path="../knockout-2.0.0.debug.js" />
/// <reference path="../jquery-ui-1.8.11.js" />
/// <reference path="../jquery-1.7.2.js" />
/// <reference path="../ajax-util.js" />
/// <reference path="../ko-protected-observable.js" />
$(function () {
    var hub = $.connection.chirpyRHub,
    $msgs = $("#chirpStream");
    hub.NewChirp = function (chirp) {

        addChirp(chirp);
    }

    function addChirp(chirp) {
        viewModel.chirps.unshift(new chirpItem(chirp.Text,
                          chirp.Id, chirp.ChirpBy.Gravataar,
                          chirp.ChirpBy.UserId));
    }

    $.connection.hub.start();
    var data = [
        new chirpItem(
            "Real cool .NET site www.dotnetcurry.com",
            1,
            'http://www.gravatar.com/avatar/8a00acdf326a8a8806ccc662a136c438.jpg',
            'minal'),
        new chirpItem(
            "Loads of web-dev tips and tricks at www.devcurry.com",
            2,
            "http://www.gravatar.com/avatar/147bacafcdb00d67d3336ecdf4078ba5.png",
            'sumit'),
        new chirpItem("Super hot, July Edition of DNC Magazine",
            3,
            "http://www.gravatar.com/avatar/0960a6d6c7c472d54d33f77f8048fa29.jpg",
            'suprotim'),
    ];

    function chirpItem(text, id, gravatar, by) {
        return {
            Text: text,
            Id: id,
            GravatarUrl: gravatar,
            By: by
        };
    }

    var viewModel = {
        // data
        chirps: ko.observableArray(data),
        currentUser: ko.observable({ UserName: "unknown", OldPassword: "" })
    }
    ko.applyBindings(viewModel);

    $.getJSON("Api/ChirpyR", null, function (data) {
        for (var i in data) {
            addChirp(i);
        }
    });

    $(document).on("click",
        "#postChirp", function () {
            var chirp = {
                "Text": $("#chirpText").val()
            };
            ajaxAdd("/Api/ChirpyR",
                ko.toJSON(chirp), function (data) {
                });
            $("#chirpText").val("");
        });
});