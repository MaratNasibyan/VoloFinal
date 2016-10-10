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
            history.pushState(null, obj.Page, obj.Url);         
        } else {
            alert("Browser does not support HTML5.");
        }
    }   

    function search() {
        $.ajax({
            url: "/Books/Index?searchString=" + $('#SearchString').val(),         
            success: function (result) {              
                ChangeUrl("Index","/Books/Index?searchString=" + $('#SearchString').val());
                $('#ProductList').html(result);
            }
        });

    }

    $(function () {
        $("#btnSearch").click(function () {
            var v = $("#SearchString").val();
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
            //console.log('page');
            var searchString = $('#SearchString').val();
            if (searchString == undefined || searchString == '') {
                searchString = '';
            } else {
                searchString = 'searchString=' + searchString;
            }
            var url = $(this).attr('href')+'&'+searchString;
            //console.log(url);
            $.ajax({
                url: url,               
                //type:'get',
                success: function (result) {
                    ChangeUrl('Index', url);
                    $('#ProductList').html(result);
                }
            });
            
        });              
        
    });
