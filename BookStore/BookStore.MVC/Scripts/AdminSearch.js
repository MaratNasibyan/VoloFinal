window.addEventListener("popstate", function (e) {
    $.ajax({
        url: location.href,
        success: function (result) {
            $('#ProductList').html(result);
        }
    });
    $('#SearchString').val() = "";
});

function ChangeUrl(page, url) {
    if (typeof (history.pushState) != "undefined") {
        var obj = { Page: page, Url: url };
        history.pushState(null, obj.Page, obj.Url);     
    } else {
        alert("Browser does not support HTML5.");
    }
}

function search() {
    $.ajax({
        url: "/Admin/Index?searchString=" + $('#SearchString').val(),
        ////type:"get",
        success: function (result) {
            ChangeUrl("Index", "/Admin/Index?searchString=" + $('#SearchString').val());
            $('#ProductList').html(result);
        }
    });
}


$(function () {
    $("#btnSearch").click(function () {
        var v = $("#SearchString").val();
        //if (v != null) {
            search();
        //}
        //else {
        //    alert("error");
        //}

    });

    $("#SearchString").keypress(function (e) {
        if (e.keyCode == 13) {
            search();
        }
    });
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

    $('body').on('click', '#ProductList .pagination a', function (event) {
        event.preventDefault();
        console.log('page');
        var searchString = $('#SearchString').val();
        if (searchString == undefined || searchString == '') {
            searchString = '';
        } else {
            searchString = '&searchString=' + searchString;
        }
        //var sort = $('#ProductList  #table  a').text();
        //var sortUrl = '&sortOption=' + sort;
        var currentsort = getUrlVars()['sortOption'];
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

    $('body').on('click',  '#ProductList table #Price', function (event) {

        event.preventDefault();

        var searchString = $('#SearchString').val();
        if (searchString == undefined || searchString == '') {
            searchString = '';
        } else {
            searchString = '&searchString=' + searchString;
        }
        var columnToSort = $(this).text();
        var currentSortOption = getUrlVars()['sortOption'];
        console.log(currentSortOption);
        var sort;
      
        switch(currentSortOption)
        {
            case "Price_ASC": sort = "sortOption=Price_DESC"; break;
            case "Price_DESC": sort = "sortOption=Price_ASC"; break;
            case "Title_ASC": sort="sortOption=Title_DESC"; break;
            case "Title_DESC": sort = "sortOption=Price_ASC"; break;
            default: sort = ""; break;
        }
              

        switch (columnToSort) {
            case 'Price':
                if (currentSortOption != 'Price_ASC' && currentSortOption != 'Price_DESC') {
                    sort = 'sortOption=Price_ASC';
                }               
                break;
            case 'Title':
                if(currentSortOption != 'Title_ASC' && currentSortOption != 'Title_DESC')
                    sort = 'sortOption=Title_ASC';                
                break;                       
            default:
                sort = '';
                break;

        }
       

        if (sort != '' & searchString != '') {
            sort = '&' + sort;
        }
        var url = '/Admin/Index?' + searchString + sort;
        $.ajax({
            url: url,
            success: function (result) {
                ChangeUrl("Index", url);
                $('#ProductList').html(result);
            }
        });
    });
   
});