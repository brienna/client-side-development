
$(document).ready(function() {
    /**************************** SETUP ****************************/
    // Initialize overlay
    $('#popup').popup({
        closeelement: '.popup_close',
    });

    /** 
     * Makes an AJAX call.
     *
     * @param type 'get' or 'post'
     * @param path API node to query
     * @return result in json
     **/
    function xhr(type, path) {
        return $.ajax({
            type: 'get',
            url: 'proxy.php',   // Note: Needed because data is on different server
            cache: false,       // Note: Set once and it will work always
            async: true,        // Note: ^^^
            dataType: 'json',
            data: path,
        }).fail(function(err) {
            console.log('Error: ' + err);
        });                     // Note: No .done() here
    }

    /**************************** SCROLLMAGIC ****************************/
    // Initialize ScrollMagic controller
    var controller = new ScrollMagic.Controller({
        globalSceneOptions: {
            triggerHook: 'onLeave'
        }
    });

    // Get all panels
    var panels = $('section.panel');

    // Create scene for every panel
    for (var i=0; i<panels.length; i++) {
        new ScrollMagic.Scene({
                triggerElement: panels[i]
            })
            .setPin(panels[i])
            .addIndicators() // debugging (requires additional plugin)
            .addTo(controller);
    }

    /**************************** ABOUT ****************************/
    // Query "About" data
    xhr('get', { path : '/about/' }).done(function(json) {
        processAbout(json);
    })

    /**
     * Processes and renders result of AJAX call to get 'About' information.
     *
     * @param data the returned result.
     **/
    function processAbout(data) {
        console.log("Processing 'About'...");

        // Get "About" data 
        var title = data.title;
        var desc = data.description;
        var quote = data.quote;
        var quoteAuthor = data.quoteAuthor;

        // Add the pieces to "About" panel
        $('#about .inner').children().first().text(title)
            .next().text(desc)
            .next().text(quote)
            .next().text("- " + quoteAuthor);
    }

    /**************************** DEGREES ****************************/
    // Query "Degrees" data
    xhr('get', { path : '/degrees/' }).done(function(json) {
        processDegrees(json);
    });

    /**
     * Processes and renders result of AJAX call to get degrees.
     *
     * @param data object representing either "graduate" or "undergraduate" degrees.
     **/
    function processDegrees(data) {
        // Process undergraduate & graduate degrees separately
        var category;
        for (var level in data) {
            category = data[level];
            if (level == "graduate") {
                certifications = category[3];
                category = category.slice(0, 3);                
            }
            
            // Get info for each degree
            console.log("Processing " + level + " degrees...");
            $.each(category, function(i, degree) {
                // Add degree title & description to Degree panel
                $('#' + level).append('<div class="degrees" data-level="' + level + '" data-name="' 
                    + degree.degreeName + '"><h3>' + degree.title + '</h3><p>' + degree.description);
            });
        }

        // Add graduate certifications
        $('#graduate').parent().append('<p>' + certifications.degreeName);

        // Allow user to click on each degree to view more details in overlay
        $('.degrees').each(function(index) {
            $(this).on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();

                // Get degree details & add to overlay
                var level = $(this).data('level');
                var name = $(this).data('name');
                for (var i = 0; i < data[level].length; i++) {
                    var degree = data[level][i];
                    if (degree.degreeName == name) {
                        $('#popup').append('<h1>' + degree.title + '</h1><p>' + degree.description + '</p>');
                        for (var j = 0; j < degree.concentrations.length; j++) {
                            $('#popup').append('<li>' + degree.concentrations[j]);
                        }

                        // Add courses available for the degree
                        var modifiedName = name.toUpperCase();
                        if (level == 'graduate') {
                            // Exception: IST is queried as IT so need to change its name here 
                            if (modifiedName == 'IST') {
                                modifiedName = 'IT';
                            }
                            modifiedName = 'MS' + modifiedName;
                        } else if (level == 'undergraduate') {
                            modifiedName = 'BS' + modifiedName;
                        } 
                        var coursePath = '/courses/degreeName=' + modifiedName;
                        $('#popup').append('<h2>Courses</h2>');
                        // Query courses with appropriately built path
                        xhr('get', { path : coursePath }).done(function(json) {
                            $.each(json.courses, function(i, course) {
                                // Add each course
                                $('#popup').append('<li>' + course);
                            })
                        });
                    }
                }

                // Show overlay
                $('#popup').popup('show');
            });
        });
    }

    // Toggle "Undergraduate" and "Graduate" views
    $('#showGrad').on('click', function() {
        $('#undergraduate').hide();
        $('#graduate').parent().show();
        $('#showUndergrad').css('color', 'gray');
        $(this).css('color', 'white');
    });
    $('#showUndergrad').on('click', function() {
        $('#graduate').parent().hide();
        $('#undergraduate').show();
        $('#showGrad').css('color', 'gray');
        $(this).css('color', 'white');
    });

    /**************************** NEWS ****************************/ 
    // Query "News" data 
    xhr('get', { path : '/news/' }).done(function(json) {
        processNews(json);
    });

    /**
     * Processes and renders result of AJAX call to get news,
     * appending previews of the two most recent articles (data is already sorted by date), 
     * to the main panel, and setting up overlay to show the full versions, 
     * as well as a composite view of all news articles.
     * 
     * @param data the returned result.
     **/
    function processNews(data) {
        console.log("Processing 'News'...");

        var numShowing = 0;  // news count tracker
        var maxNumShowing = 2;  // max num of news stories to show on home page
        var ajaxPath; // path for querying later
        var desc; // abbreviated news description 

        // If news exists from recent year, process it
        if (data.year) {
            try {
                for (i = 0; numShowing < maxNumShowing; i++) {
                    // Get the parts of each news article                
                    desc = data.year[i].description.split(" ").slice(0,20).join(" ");

                    // Add to correct news column
                    ajaxPath = ('/news/year/date=' + data.year[i].date).replace(" ", "%20"); 
                    $('.news_column:nth-child(' + (i+1) + ')').append('<a href="#" data-ajaxpath="' + 
                        ajaxPath +'"><h1>' + data.year[i].title + '</h1><p>' + desc + '...</p></a>');

                    // Update news count tracker
                    numShowing++;
                }
            } catch(error) {
                console.error(error);
                console.log("Hasn't been tested, as json didn't have any /year/ " +
                    "but now it does, so fix error, then remove try & catch blocks");
            }
        }

        // If older news exists & still need more news to show, process it
        if (data.older) {
            for (i = 0; numShowing < maxNumShowing; i++) {
                desc = data.older[i].description.split(" ").slice(0,20).join(" ");

                // Add to correct news column
                ajaxPath = ('/news/older/date=' + data.older[i].date).replace(" ", "%20"); 
                $('.news_column:nth-child(' + (i+1) + ')').append('<a href="#" data-ajaxpath="' +
                    ajaxPath +'"><h1>' + data.older[i].title + '</h1><p>' + desc + '...</p></a>');

                // Update news count tracker
                numShowing++;
            }
        }

        // Append "More news >>" to news div
        var moreNewsHtml = '<a href="#" class="more_news" data-ajaxpath="/news/"><p>More news >>></p></a>';
        $('#news').append(moreNewsHtml).children().last().on('click', function() {
            showAllNews();
        });

        // Load full article for selected news onto overlay 
        $('.news_column a').each(function() {
            // If user clicks the anchor tag wrapping the contents of each news-column,
            $(this).on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                
                // Get data based on path specified in anchor tag's data attribute
                // TO BE CHANGED: Probably don't need to make another query, can filter data by date
                xhr('get', { path : $(this).data('ajaxpath') }).done(function(json) {
                    $('#popup').append('<h1>' + json.title + '</h1><p>' + json.date + '<br><br>' + json.description + '...</p>');
                    // Also append "More news >>>" to overlay
                    $('#popup').append(moreNewsHtml).children().last().on('click', function() {
                        showAllNews();
                    });
                    // Show overlay
                    $('#popup').popup('show');
                });
            });
        });

        /**
         * Appends all news articles onto overlay, then shows overlay.
         **/
        function showAllNews() {
            // Reset overlay, leaving only its close button
            $('#popup').children().slice(1).remove();
            // Add all news articles from both /news/year/ and /news/older/
            for (var category in data) {
                if (data.hasOwnProperty(category)) {
                    $.each(data[category], function(i, article) {
                        $('#popup').append('<h1>' + article.title + '</h1><p>' + article.date + '<br><br>' + (article.description ? article.description : "" ) + '</p><hr/>'); // Note: Shows description only if not null
                    });
                }
            }
            // Show overlay
            $('#popup').popup('show');
        }
    }

});