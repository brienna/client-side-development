
$(document).ready(function() {
    /**************************** SETUP ****************************/
    // Initialize overlay
    $('#popup').popup({
        closeelement: '.popup_close',
        scrolllock: true // Disables scrolling of background content while the popup is visible
    });

    // Initialize ScrollMagic controller
    var controller = new ScrollMagic.Controller();

    // Initialize "Employment" Google Map
    $("#map").googleMap({
      zoom: 4, // Initial zoom level 
      type: "HYBRID", 
      scrollwheel: false // Disable scroll scaling 
    });

    /** 
     * Makes an AJAX call.
     *
     * @param type 'get' or 'post'
     * @param path API node to query
     * @return result in json
     **/
    function xhr(type, dataType, path) {
        return $.ajax({
            type: type,
            url: 'proxy.php',   // Note: Needed because data is on different server
            cache: false,       // Note: Set once and it will work always
            async: true,        // Note: ^^^
            dataType: dataType,
            data: path,
        }).fail(function(err) {
            console.log('Error: ' + err);
        });                     // Note: No .done() here
    }

    /**************************** ABOUT ****************************/
    // Query "About" data
    xhr('get', 'json', { path : '/about/' }).done(function(json) {
        processAbout(json);
    });

    /**
     * Processes and renders result of AJAX call to get 'About' information.
     *
     * @param data the returned result.
     **/
    function processAbout(data) {
        console.log("Processing about...");

        // Add the data pieces to "About" panel
        $('#about .inner').children().first().text(data.title)
            .next().text(data.description)
            .next().text(data.quote)
            .next().text("- " + data.quoteAuthor);
    }

    /**************************** DEGREES ****************************/
    // Query "Degrees" data
    xhr('get', 'json', { path : '/degrees/' }).done(function(json) {
        processDegrees(json);
    });

    /**
     * Processes and renders result of AJAX call to get degrees.
     *
     * @param data object representing either "graduate" or "undergraduate" degrees.
     **/
    function processDegrees(data) {
        var category;
        var node;

        // Process undergraduate & graduate degrees separately,
        for (var level in data) {
            console.log("Processing " + level + " degrees...");
            category = data[level];

            // If degrees are graduate,
            if (level == "graduate") {
                // Set aside certifications to be processed separately
                processCertificates(category[3].availableCertificates);
                category = category.slice(0, 3);
                // Specify degrees to be appended to graduate degrees sub-sub-section
                node = '#degrees';
            }

            // If degrees are undergraduate,
            if (level == "undergraduate") {
                // Specify degrees to be appended to the majors sub-sub-section
                node = '#majors';
            }

            // Add degrees to appropriate view in Degrees panel
            $.each(category, function(i, degree) {
                $(node).append('<div class="clickable degree" data-level="' + level + '" data-name="' + 
                    degree.degreeName + '"><h4>' + degree.title + '</h4><p>' + degree.description);
            });
        }

        // Allow user to click on each degree to view more details in overlay
        $('.degree').each(function(index) {
            $(this).on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();

                // Get degree details & add to overlay
                var level = $(this).data('level');
                var name = $(this).data('name');
                for (var i = 0; i < data[level].length; i++) {
                    var degree = data[level][i];
                    if (degree.degreeName == name) {
                        $('#popup').append('<h1>' + degree.title + '</h1><p>' + degree.description + '</p><h2>Concentrations</h2><ul>');
                        for (var j = 0; j < degree.concentrations.length; j++) {
                            $('#popup').append('<li>' + degree.concentrations[j]);
                        }

                        // Get courses available for the degree
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
                        // Set heading and event listener to load courses
                        $('#popup').append('<h2 class="btn">View Available Courses</h2>').find('h2').last().on('click', function() {
                            // Change heading
                            $(this).removeClass('btn').text('Available Courses');
                            // Load and show courses
                            getCourses(coursePath);
                            // Remove event handler to prevent further ajax calls 
                            $(this).off();
                        });

                        break; // end data loop looking for degree that was clicked
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

    /**************************** GRADUATE CERTIFICATES ****************************/

    function processCertificates(data) {
        console.log("Processing graduate certificates...");

        var link;
        for (var x = 0; x < data.length; x++) {
            if (data[x] == "Web Development Advanced certificate") {
                link = "http://www.rit.edu/programs/web-development-adv-cert";
            }
            if (data[x] == "Networking,Planning and Design Advanced Cerificate") {
                link = "http://www.rit.edu/programs/networking-planning-and-design-adv-cert";
            }
            $('#certificates').append('<a href="' + link + '" target="_blank"><div class="clickable certificate">' + data[x]);
        }
    }

    // Toggle "Degrees" and "Certificates" views
    $('#showDegrees').on('click', function() {
        $('#certificates').hide();
        $('#degrees').show();
        $('#showCertificates').css('color', 'gray');
        $(this).css('color', 'white');
    });
    $('#showCertificates').on('click', function() {
        $('#degrees').hide();
        $('#certificates').show();
        $('#showDegrees').css('color', 'gray');
        $(this).css('color', 'white');
    });

    /**************************** UNDERGRADUATE MINORS ****************************/
    // Query minors data
    xhr('get', 'json', { path : '/minors/' }).done(function(json) {
        processMinors(json.UgMinors);
    });

    function processMinors(data) {
        console.log("Processing undergraduate minors...");

        $.each(data, function(i, minor) {
            $('#minors').append('<div class="minor clickable" data-name="' + minor.name + '"><h4>' + minor.title);
        });

        // Allow user to click on each minor to view more details in overlay
        $('.minor').each(function() {
            $(this).on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();

                for (var i = 0; i < data.length; i++) {
                    var minor = data[i];
                    if (minor.name == $(this).data('name')) {
                        // Add details to overlay
                        $('#popup').append('<h1>' + minor.title + '</h1><p>' + minor.description + '<h2>Courses</h2><ul class="list-group">');
                        for (var j = 0; j < minor.courses.length; j++) {
                            $('#popup ul').append('<li class="course list-group-item" data-name="' + minor.courses[j] + '"><p>' + minor.courses[j] + '</p>');
                        }
                        break; // end data loop looking for minor that was clicked
                    }
                }

                // Allow user to click any course to see or hide details
                enableCourseDetails();

                // Show overlay
                $('#popup').popup('show');
            });
        });
    }

    // Toggle "Majors" and "Minors" views
    $('#showMajors').on('click', function() {
        $('#minors').hide();
        $('#majors').show();
        $('#showMinors').css('color', 'gray');
        $(this).css('color', 'white');
    });
    $('#showMinors').on('click', function() {
        $('#majors').hide();
        $('#minors').show();
        $('#showMajors').css('color', 'gray');
        $(this).css('color', 'white');
    });

    /**************************** COURSES ****************************/

    function getCourses(coursePath) {
        // Query courses 
        xhr('get', 'json', { path : coursePath }).done(function(json) {
            processCourses(json.courses);

            // Allow user to click any course to see or hide details
            enableCourseDetails();
        });
    }

    function processCourses(courses) {
        // Add each course to names array if its name is valid and unique
        var names = [];
        $.each(courses, function(i, course) {
            if (/[A-Z]{3,4}-(?!0)\d{2,3}/.test(course)) {
                if ($.inArray(course, names) === -1) {
                    names.push(course);
                }
            }
        });

        // Sort courses by alphabetical order
        names.sort();

        // Add each course to overlay  
        $.each(names, function(i, course) {
            $('#popup').append('<li class="list-group-item course" data-name="' + course + '"><p>' + course + '</p>');
        });
    }

    // Allow user to click any course (in any view) to see or hide details
    function enableCourseDetails() {
        console.log($('.course').length);
        $('.course').each(function(i, element) {
            $(element).on('click', function() {
                // If course data is showing, erase
                if ($(element).has('#courseDetails').length) {
                    $(element).children().slice(1).remove();
                } else {
                    // Otherwise query course data and show
                    xhr('get', 'json', { path : '/course/courseID=' + $(element).data('name') }).done(function(json) {
                        console.log(json);
                        var html = '<div id="courseDetails"><p><b>' + json.title + '</b></p>';
                        if (json.description) {
                            html = html + '<p>' + json.description + '</p>';
                        } 
                        $(element).append(html + '</div>');
                    });
                }
            });
        });
    }

    /**************************** PEOPLE ****************************/
    // Query "people" data
    xhr('get', 'json', { path : '/people/'}).done(function(json) {
        processPeople(json);
    });

    function processPeople(data) {
        console.log("Processing people...");

        // Add headings
        $('#people .inner').children().first().text(data.title).next().text(data.subTitle);

        // Add faculty members
        $.each(data.faculty, function() {
            $('#people .inner').find('ul').append('<li class="clickable person col-sm-2 faculty" data-username="' + this.username + '">' + this.name);
        }); 

        // Add staff members
        $.each(data.staff, function() {
            $('#people .inner').find('ul').append('<li class="clickable person col-sm-2 staff" data-username="' + this.username + '">' + this.name);
        });

        // Set faculty as default view
        $('.staff').hide();

        // Allow user to click on person to see more details
        $('.person').each(function() {
            $(this).on('click', function() {
                var thisPerson = $(this);

                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();

                var category; 
                if (thisPerson.hasClass('faculty')) {
                    category = data.faculty;
                } else if (thisPerson.hasClass('staff')) {
                    category = data.staff;
                }

                var person;
                $.each(category, function(i, item) {
                    // After finding the correct match in the data, populate overlay with person's details
                    if (item.username == thisPerson.data('username')) {
                        person = item;
                        return false;  // end data loop looking for person that was clicked
                    }
                });

                // Generate heading
                var html = '<h1>' + person.name + '</h1><h2>' + person.title + '</h2>';
                if (person.tagline) { 
                    html = html + '<h2>' + person.tagline + '</h2>';
                }
                // Generate left column with image 
                html = html + '<div class="row"><div class="col-md-6"><img src="' + person.imagePath + '" width="50%"/></div>';
                // Generate right column with small details
                html = html + '<div class="col-md-6"><ul>';
                if (person.office) {
                    html = html + '<li>' + person.office + '</li>';
                }
                if (person.phone) {
                    html = html + '<li>' + person.phone + '</li>';
                }
                if (person.website) {
                    html = html + '<li><a href="' + person.website + '">' + person.website + '</a></li>';
                }
                if (person.email) {
                    html = html + '<li>' + person.email + '</li>';
                }
                if (person.facebook) {
                    html = html + '<li>' + person.facebook + '</li>';
                }
                if (person.twitter) {
                    html = html + '<li>' + person.twitter + '</li>';
                }
                if (person.interestArea) {
                   html = html + '<li>Interest areas: ' + person.interestArea.toUpperCase() + '</li>';
                }

                html = html + '</div></div>';
                // Generate description panel with queried data
                html = html + '<div class="row"><div class="col-md-12">' + '</div></div>';
                $('#popup').append(html);

                // Show overlay
                $('#popup').popup('show');
            });
        });
    }

    // Toggle "Majors" and "Minors" views
    $('#showFaculty').on('click', function() {
        $('.staff').hide();
        $('.faculty').show();
        $('#showStaff').css('color', 'gray');
        $(this).css('color', 'white');
    });
    $('#showStaff').on('click', function() {
        $('.faculty').hide();
        $('.staff').show();
        $('#showFaculty').css('color', 'gray');
        $(this).css('color', 'white');
    });

    /**************************** EMPLOYMENT ****************************/ 

    xhr('get', 'json', { path : '/employment/' }).done(function(json) {
        processEmployment(json);

        // Query employment locations to mark on map
        xhr('get', 'json', { path : '/location/' }).done(function(results) {
            // Process locations along with details for info window
            processLocations(results, json.employmentTable.professionalEmploymentInformation);
        });
    });

    function processEmployment(data) {
        console.log("Processing employment...");
        console.log(data);

        // Get data pieces
        var heading = data.introduction.title;
        var empHeading = data.introduction.content[0].title;
        var empDesc = data.introduction.content[0].description;
        empDesc = empDesc.replace('95% of our students landing a job within the first 6 months after graduation', 
            '<span class="highlight">95% of our students landing a job within the first 6 months after graduation</span>');
        var coopHeading = data.introduction.content[1].title;
        var coopDesc = data.introduction.content[1].description;
        coopDesc = coopDesc.replace('resources page', '<span class="clickable highlight">resources page</span>');
        var coopTableHeading = data.coopTable.title;
        var coopTableInfo = data.coopTable.coopInformation; // 2D array
        var empTableHeading = data.employmentTable.title;
        var empTableInfo = data.employmentTable.professionalEmploymentInformation;

        var degreeStatsHeading = data.degreeStatistics.title;
        var degreeStats = data.degreeStatistics.statistics; // 2D array
        var careersHeading = data.careers.title; 
        var careers = data.careers.careerNames; // 1D array
        var employersHeading = data.employers.title;
        var employers = data.employers.employerNames; // 1D array

        // Add employer & co-op headings & descriptions to "Work" panel
        $('#work h1').text(heading);
        $('#work .col-md-6').first().append('<h2>' + empHeading + '</h2><p>' + empDesc + '</p>');
        $('#work .col-md-6').last().append('<h2>' + coopHeading + '</h2><p>' + coopDesc + '</p>');
        // If the user clicks on "Resources," take user to "Resources" panel
        $('#work .col-md-6').last().find('span').on('click', function() {
            wipeAnimation.seek(6);  // based on seconds defined in wipeAnimation 
        });

        // Add coop & employment table buttons
        var coopTableBtn = '<div class="btn" id="coop_table"><p>' + coopTableHeading + '</p></div>';
        var empTableBtn = '<div class="btn" id="emp_table"><p>' + empTableHeading + '</p></div>';
        $('#work .inner').append('<div class="row">' + coopTableBtn + empTableBtn + '</div>');
        
        // If user clicks on coop table button, show coop table in overlay
        $('#coop_table').on('click', function() {
            // Generate coop table HTML
            var coopTable = '<div class="table-responsive"><table class="table table-hover"><thead><tr><th>DEGREE</th>' +
                '<th>EMPLOYER</th><th>CITY</th><th>TERM</th></thead><tbody>';
            $.each(coopTableInfo, function(i, row) {
                coopTable = coopTable + '<tr><td>' + row.degree + '</td><td>' + row.employer + 
                    '</td><td>' + row.city + '</td><td>' + row.term + '</td></tr>';
            });
            coopTable = coopTable + '</tbody></table></div>';

            // Reset overlay, leaving only its close button, then append table & show overlay
            $('#popup').children().slice(1).remove();
            $('#popup').append(coopTable).popup('show');
        });

        // If user clicks on emp table button, show employment table in overlay
        $('#emp_table').on('click', function() {
            // Generate employment table HTML
            var empTable = '<div class="table-responsive"><table class="table table-hover"><thead><tr><th>DEGREE</th>' +
                '<th>EMPLOYER</th><th>TITLE</th><th>CITY</th><th>START DATE</th></thead><tbody>';
            $.each(empTableInfo, function(i, row) {
                empTable = empTable + '<tr><td>' + row.degree + '</td><td>' + row.employer + '</td><td>' + row.title + 
                    '</td><td>' + row.city + '</td><td>' + row.startDate + '</td></tr>';
            });
            empTable = empTable + '</tbody></table></div>';

            // Reset overlay, leaving only its close button, then append table & show overlay
            $('#popup').children().slice(1).remove();
            $('#popup').append(empTable).popup('show');
        });
    }

    /**
     * Processes and renders array of locations,
     * appending each location as a marker to map.
     * 
     * @param locations array of locations.
     * @param details array of employment details.
     **/
    function processLocations(locations, details) {
        console.log("Processing employment locations...");
        // For each location,
        $.each(locations, function(i, location) {
            var locCity = (location.city + location.state).toUpperCase().trim();
            // Initialize its info window html
            var info = '<div id="map_info">';
            // Search employment details for any matching locations
            $.each(details, function(i, detail) {
                var detailCity = detail.city.toUpperCase().replace(/[,]/g, "").trim();
                // If a matching location in details has been found,
                if (~locCity.indexOf(detailCity) || ~detailCity.indexOf(locCity)) {
                    // Add details to location's info window html
                    info = info + '<hr/>';
                    var employer = "<p>Employer: " + detail.employer + '</p>';
                    var jobTitle = "<p>Job Title: " + detail.title  + '</p>';
                    var degree = "<p>Degree: " + detail.degree  + '</p>';
                    var startDate = "<p>Start Date: " + detail.startDate + '</p>';
                    info = info + employer + jobTitle + degree + startDate;
                }
            });

            // Add marker & its info window to map
            $("#map").addMarker({
                coords: [location.latitude, location.longitude],
                title: locCity,
                text: info
            });
        });
    }

    /**************************** NEWS ****************************/ 
    // Query "News" data 
    xhr('get', 'json', { path : '/news/' }).done(function(json) {
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
        console.log("Processing news...");

        var numShowing = 0;  // news count tracker
        var maxNumShowing = 2;  // max num of news stories to show on home page
        var ajaxPath; // path for querying later
        var desc; // abbreviated news description 

        // If news exists from recent year, process it
        if (data.year) {
            try {
                for (var i = 0; numShowing < maxNumShowing; i++) {
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
            for (var j = 0; numShowing < maxNumShowing; j++) {
                desc = data.older[j].description.split(" ").slice(0,20).join(" ");

                // Add to correct news column
                ajaxPath = ('/news/older/date=' + data.older[j].date).replace(" ", "%20"); 
                $('.news_column:nth-child(' + (j+1) + ')').append('<a href="#" data-ajaxpath="' +
                    ajaxPath +'"><h1>' + data.older[j].title + '</h1><p>' + desc + '...</p></a>');

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
                xhr('get', 'json', { path : $(this).data('ajaxpath') }).done(function(json) {
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

    /**************************** CONTACT FORM ****************************/
    // Query Contact Form  
    xhr('get', 'html', { path : '/contactForm/' }).done(function(results) {
        $('#contactForm').append(results);
    });

    /**************************** FOOTER ****************************/

    // Query "Footer" data 
    xhr('get', 'json', { path : '/footer/' }).done(function(json) {
        processFooter(json);
    });

    function processFooter(data) {
        console.log(data);

        var social = data.social;
    }

    /**************************** SCROLLING ****************************/

    // Define movement of panels
    var wipeAnimation = new TimelineMax()
        .to("#slideContainer", 1,   {x: "-12.5%"})      // to Degrees
        .to("#slideContainer", 1,   {x: "-25%"})        // to People
        .to("#slideContainer", 1,   {x: "-37.5%"})      // to Research
        .to("#slideContainer", 1,   {x: "-50%"})        // to Employment
        .to("#slideContainer", 1,   {x: "-62.5%"})      // to Work
        .to("#slideContainer", 1,   {x: "-75%"})        // to Resources

    // Create scene to pin and link animation
    new ScrollMagic.Scene({
            triggerElement: "#pinContainer",
            triggerHook: "onLeave",
            duration: "600%"  // slows down scrolling
        })
        .setPin("#pinContainer")
        .setTween(wipeAnimation)
        .addIndicators() 
        .addTo(controller);

});