window.addEventListener("popstate", function (e) {
    $.ajax({
        url: location.href,
        success: function (result) {
            $('#ProductList').html(result);
        }
    });
});

function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState('Index', obj.Page, obj.Url);
    } else {
        alert("Browser does not support HTML5.");
    }
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function search() {
    $.ajax({
        url: "/Books/Index?searchString=" + $('#SearchString').val(),
        ////type:"get",
        success: function (result) {
            ChangeUrl("Index", "/Books/Index?searchString=" + $('#SearchString').val());
            $('#ProductList').html(result);
        }
    });
}


$(function () {
    $("#btnSearch").click(function () {
        var v = $("#SearchString").val;
        if (v != null) {
            search();
        }
        else {
            alert("error");
        }

    });

    $("#SearchString").keypress(function (e) {
        if (e.keyCode == 13) {
            search();
        }
    });


    $('body').on('click', '#ProductList .pagination a', function (event) {
        event.preventDefault();
        console.log('page');
        var searchString = $('#SearchString').val();
        if (searchString == undefined || searchString == '') {
            searchString = '';
        } else {
            searchString = '&searchString=' + searchString;
        }
        var url = $(this).attr('href') + searchString;
        console.log(url);
        $.ajax({
            url: url,
            success: function (result) {
                ChangeUrl("Index", url);
                $('#ProductList').html(result);
            }
        });


    });



});