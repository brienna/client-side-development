
$(document).ready(function() {
    // Get the /about/
    $.ajax({
        type: 'get',
        url: 'proxy.php',  // Note: Needed because data is on different server
        data: {path: '/about/'},  // Note: 'path' is a variable in proxy.php
        dataType: 'json',
    }).done(function(json) {
        console.log(json.description);
    });

    // Get undergraduate degrees
    $.ajax({
        type: 'get',
        url: 'proxy.php',
        data: {path: '/degrees/undergraduate'},
        dataType: 'json',
    }).done(function(json) {
        console.log('called');
        console.log(json.undergraduate[0].degreeName);
        $.each(json.undergraduate, function(i, item) {
            $('#content').append('<h2>' + item.title + '</h2>');
        });
    });

});