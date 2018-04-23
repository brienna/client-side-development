// Note: jQuery docs recommend to use Modernizr to detect features
if (!Modernizr.csstransforms3d) {
    window.location = "legacy.html";
} 

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
        $('#about .inner').children().first().text(data.title).next().text(data.description);
        $('#quote').text(data.quote)
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
                $(node).append('<div class="clickable degree col-sm-4" data-level="' + level + '" data-name="' + 
                    degree.degreeName + '"><h4>' + degree.title + '</h4><p>' + degree.description);
            });
        }

        // Make each degree box equal in height
        $('.degree').matchHeight(false);

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

        // Set Undergraduates as default view
        $('#showUndergrad').addClass('clicked').removeClass('clickable');
        $('#graduate').parent().hide();

        // Toggle "Undergraduate" and "Graduate" views
        $('#showGrad').on('click', function() {
            $('#undergraduate').hide();
            $('#graduate').parent().show();
            $('#showUndergrad').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
        $('#showUndergrad').on('click', function() {
            $('#graduate').parent().hide();
            $('#undergraduate').show();
            $('#showGrad').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
    }

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
            $('#certificates').append('<a href="' + link + '" target="_blank" class="col-sm-6 certificate clickable">' + data[x]);
    
        }

        // Make each degree box equal in height
        $('.certificate').matchHeight(false);

        // Set Degrees as default view
        $('#showDegrees').addClass('clicked').removeClass('clickable');
        $('#certificates').hide();

        // Toggle "Degrees" and "Certificates" views
        $('#showDegrees').on('click', function() {
            $('#certificates').hide();
            $('#degrees').show();
            $('#showCertificates').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
        $('#showCertificates').on('click', function() {
            $('#degrees').hide();
            $('#certificates').show();
            $('#showDegrees').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
    }

    /**************************** UNDERGRADUATE MINORS ****************************/
    // Query minors data
    xhr('get', 'json', { path : '/minors/' }).done(function(json) {
        processMinors(json.UgMinors);
    });

    function processMinors(data) {
        console.log("Processing undergraduate minors...");

        $.each(data, function(i, minor) {
            $('#minors').append('<div class="minor clickable col-sm-4" data-name="' + minor.name + '"><h4>' + minor.title);
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

        // Make each minor box equal in height
        $('.minor').matchHeight(false);

        // Set "Majors" as default view
        $('#showMajors').addClass('clicked').removeClass('clickable');
        $('#minors').hide();

        // Toggle "Majors" and "Minors" views
        $('#showMajors').on('click', function() {
            $('#minors').hide();
            $('#majors').show();
            $('#showMinors').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
        $('#showMinors').on('click', function() {
            $('#majors').hide();
            $('#minors').show();
            $('#showMajors').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
    }

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
        console.log("Processing courses...");

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
        $('.course').each(function(i, element) {
            $(element).on('click', function() {
                // If course data has been loaded, toggle visibility
                if ($(element).has('#courseDetails').length) {
                    $(element).children().slice(1).toggle();
                } else {
                    // Otherwise query course data and show
                    xhr('get', 'json', { path : '/course/courseID=' + $(element).data('name') }).done(function(json) {
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

        // Set faculty as default view
        $('#showFaculty').addClass('clicked').removeClass('clickable');
        $('.staff').hide();

        // Toggle "Majors" and "Minors" views
        $('#showFaculty').on('click', function() {
            $('.staff').hide();
            $('.faculty').show();
            $('#showStaff').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
        $('#showStaff').on('click', function() {
            $('.faculty').hide();
            $('.staff').show();
            $('#showFaculty').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
    }

    /**************************** RESEARCH ****************************/ 
    xhr('get', 'json', { path : '/research/' }).done(function(json) {
        processResearch(json);
    });

    function processResearch(data) {
        console.log("Processing research...");

        // Add research by interest area
        $.each(data.byInterestArea, function() {
            $('#research .inner').find('ul').append('<li class="clickable research col-sm-3 by_interest_area" data-interestarea="' + this.areaName + '">' + this.areaName);
        }); 

        // Add research by faculty name
        $.each(data.byFaculty, function() {
            $('#research .inner').find('ul').append('<li class="clickable research col-sm-3 by_faculty" data-username="' + this.username + '">' + this.facultyName);
        });

        // Allow user to click on research interest area or faculty name to see more details
        $('.research').each(function() {
            $(this).on('click', function() {
                var thisResearchItem = $(this);
                
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();

                // Generate HTML (note of interest: used alternative ways to find matches, grep/each)
                var html;
                if (thisResearchItem.hasClass('by_faculty')) {
                    // Find correct match in data
                    var match = $.grep(data.byFaculty, function(item, i) {
                        return (item.username == thisResearchItem.data('username'));
                    })[0];
                    // Populate overlay with research citations of the matched faculty
                    html = '<h1>' + match.facultyName + '</h1><ul>';
                    $.each(match.citations, function(i, citation) {
                        html = html + '<li>' + citation + '</li>';
                    });
                    $('#popup').append(html + '</ul>');
                    // Show overlay
                    $('#popup').popup('show');
                } else if (thisResearchItem.hasClass('by_interest_area')) {
                    $.each(data.byInterestArea, function(i, item) {
                        // After finding the correct match in the data, populate overlay with research citations
                        if (item.areaName == $(thisResearchItem).data('interestarea')) { // NOTE: cannot use caps with data attr
                            html = '<h1>' + item.areaName + '</h1><ul>';
                            $.each(item.citations, function(i, citation) {
                                html = html + '<li>' + citation + '</li>';
                            });
                            $('#popup').append(html + '</ul>');
                            // Show overlay
                            $('#popup').popup('show');
                            return false;  // end data loop looking for person that was clicked
                        }
                    });
                }
            });
        });

        // Set interest areas as default view
        $('#showResearchInterest').addClass('clicked').removeClass('clickable');
        $('.by_faculty').hide();

        // Toggle "by_interest_area" and "by_faculty" views
        $('#showResearchInterest').on('click', function() {
            $('.by_faculty').hide();
            $('.by_interest_area').show();
            $('#showResearchFaculty').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
        $('#showResearchFaculty').on('click', function() {
            $('.by_interest_area').hide();
            $('.by_faculty').show();
            $('#showResearchInterest').removeClass('clicked').addClass('clickable');
            $(this).addClass('clicked').removeClass('clickable');
        });
    }


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
        var coopTableInfo = data.coopTable.coopInformation; 
        var empTableHeading = data.employmentTable.title;
        var empTableInfo = data.employmentTable.professionalEmploymentInformation;

        var degreeStatsHeading = data.degreeStatistics.title;
        var degreeStats = data.degreeStatistics.statistics; 
        var careersHeading = data.careers.title; 
        var careers = data.careers.careerNames; 
        var employersHeading = data.employers.title;
        var employers = data.employers.employerNames; 

        // Add statistics to second panel
        $('#employment2 .inner').append('<h1>' + degreeStatsHeading + '</h1>');
        var statsHtml = '<div class="stats">';
        $.each(degreeStats, function(i, stat) {
            statsHtml = statsHtml + '<div class="stat"><p>' + stat.value + '</p><p>' + stat.description + '</div>';
        });
        $('#employment2 .inner').append(statsHtml + '</div>'); 

        // Add careers & employers to second panel
        var html = '<div class="row"><div class="col-md-6"><ul id="careers"><h2>' + careersHeading + '</h2>';
        $.each(careers, function(index, career) {
            html = html + '<li>' + career + '</li>';
        });
        html = html + '</ul></div><div class="col-md-6><ul id="employers"><h2>' + employersHeading + '</h2>';
        $.each(employers, function(index, employer) {
            html = html + '<li>' + employer + '</li>';
        });
        html = html + '</ul></div></div>';
        $('#employment2 .inner').append(html);

        // Add employer & co-op headings & descriptions to first panel
        $('#employment1 h1').text(heading);
        $('#employment1 .col-md-6').first().append('<h2>' + empHeading + '</h2><p>' + empDesc + '</p>');
        $('#employment1 .col-md-6').last().append('<h2>' + coopHeading + '</h2><p>' + coopDesc + '</p>');
        // If the user clicks on "Resources," take user to "Resources" panel
        $('#employment1 .col-md-6').last().find('span').on('click', function() {
            wipeAnimation.seek(7);  // based on seconds defined in wipeAnimation 
            //wipeAnimation.play();
            //TweenLite.to(window, 0.8, {scrollTo:"#resources", autoKill:false, ease:Power4.easeInOut});
        });

        // Add coop & employment table buttons to first panel
        var coopTableDiv = '<div class="col-md-6 clickable" id="coop_table"><p>' + coopTableHeading + '</p></div>';
        var empTableDiv = '<div class="col-md-6 clickable" id="emp_table"><p>' + empTableHeading + '</p></div>';
        $('#employment1 .inner').children().last().append(empTableDiv + coopTableDiv);
        
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

        // Make stat boxes equal height
        $('.stat').matchHeight(false);
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
                    desc = data.year[i].description.split(" ").slice(0,30).join(" ");

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
                desc = data.older[j].description.split(" ").slice(0,30).join(" ");

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
        $('#news').parent().append(moreNewsHtml).children().last().on('click', function() {
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
                        $('#popup').append('<h1>' + article.title + '</h1><p>' + article.date + '<br><br>' + 
                            (article.description ? article.description : "" ) + '</p><hr/>'); // Note: Shows description only if not null
                    });
                }
            }
            // Show overlay
            $('#popup').popup('show');
        }
    }

    /**************************** RESOURCES ****************************/
    // Query Resources data  
    xhr('get', 'json', { path : '/resources/' }).done(function(json) {
        processResources(json);
    });

    function processResources(resources) {
        console.log("Processing resources...");

        // Add title & subtitle
        var title = resources.title;
        var subtitle = resources.subTitle;
        $('#resources h1').text(title).next().text(subtitle);

        // Get resources
        var forms = resources.forms;
        var tutorsAndLabInfo = resources.tutorsAndLabInformation;
        var advising = resources.studentServices;
        var studyAbroad = resources.studyAbroad;
        var ambassadors = resources.studentAmbassadors;
        var coop = resources.coopEnrollment;
        
        // Add resources to panel & overlay
        $('.resource')
            .first().append('<p>Forms</p>').on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                // Populate overlay
                var html = '<h1>Forms</h1>';
                $.each(forms, function(i, category) {
                    if (i == 'graduateForms') {
                        html = html + '<h2>Graduate</h2><ul>';
                    } else if (i == 'undergraduateForms') {
                        html = html + '<h2>Undergraduate</h2><ul>';
                    }
                    $.each(category, function(j, form) {
                        html = html + '<li><a href="http://ist.rit.edu/' + form.href + '" target="_blank">' + form.formName + '</a></li>'; 
                    });
                    html = html + '</ul>';
                });
                $('#popup').append(html);
                // Show overlay
                $('#popup').popup('show');
            })
            .next().append('<p>' + tutorsAndLabInfo.title + '</p>').on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                // Populate overlay
                $('#popup').append('<h1>' + tutorsAndLabInfo.title + '</h1><p>' + tutorsAndLabInfo.description + '</p>' + 
                    '<a href="' + tutorsAndLabInfo.tutoringLabHoursLink + '" target="_blank">Lab Hours and TA Schedule</a>');
                // Show overlay
                $('#popup').popup('show');
            })
            .next().append('<p>' + advising.title + '</p>').on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                // Populate overlay
                var aAdvisors = advising.academicAdvisors;
                var fAdvisors = advising.facultyAdvisors;
                var pAdvisors = advising.professonalAdvisors;
                var iAdvisors = advising.istMinorAdvising;
                var html = '<h1>' + advising.title + '</h1>' + '<h2>' + aAdvisors.title + '</h2><p>' + aAdvisors.description + 
                    '</p><p><a href="' + aAdvisors.faq.contentHref + '" target="_blank">' + aAdvisors.faq.title + '</a></p><h2>' + 
                    fAdvisors.title + '</h2><p>' + fAdvisors.description + '</p><h2>' + pAdvisors.title + '</h2><ul>';
                $.each(pAdvisors.advisorInformation, function(i, advisor) {
                    html = html + '<li>' + advisor.name + ' (<a href="mailto:' + advisor.email + '">' + advisor.email + '</a>): ' + 
                        advisor.department + '</li>';
                });
                html = html + '</ul><h2>' + iAdvisors.title + '</h2><ul>';
                $.each(iAdvisors.minorAdvisorInformation, function(i, advisor) {
                    html = html + '<li>' + advisor.advisor + ' (<a href="mailto:' + advisor.email + '">' + advisor.email.trim() + '</a>): ' + 
                        advisor.title + '</li>';
                });
                $('#popup').append(html + '</ul>');
                // Show overlay
                $('#popup').popup('show');
            })
            .next().append('<p>' + studyAbroad.title + '</p>').on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                // Populate overlay
                var html = '<h1>' + studyAbroad.title + '</h1><p>' + studyAbroad.description + '</p><h2>Places</h2><ul>';
                $.each(studyAbroad.places, function(i, place) {
                    html = html + '<li>' + place.nameOfPlace + ' — ' + place.description + '</li>';
                });
                $('#popup').append(html + '</ul>');
                // Show overlay
                $('#popup').popup('show');
            })
            .next().append('<p>' + ambassadors.title + '</p>').on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                // Populate overlay
                var html = '<h1>' + ambassadors.title + '</h1><img src="' + ambassadors.ambassadorsImageSource + '"/>';
                $.each(ambassadors.subSectionContent, function(i, section) {
                    var description = section.description;
                    if (section.title == "apply") {
                        // Activate form link
                        description = section.description.replace(/apply/, '<a href="' + ambassadors.applicationFormLink + '" target="_blank">apply</a>');
                    }
                    html = html + '<h2>' + section.title + '</h2><p>' + description + '</p>';
                });
                $('#popup').append(html + '<p>' + ambassadors.note + '</p>');
                // Set image size
                $('#popup').find('h1 + img').css({'width': '60%', 'margin': '0 auto', 'display': 'block', 'padding-bottom': '40px'});
                // Show overlay
                $('#popup').popup('show');
            })
            .next().append('<p>' + coop.title + '</p>').on('click', function() {
                // Reset overlay, leaving only its close button
                $('#popup').children().slice(1).remove();
                // Populate overlay
                var html = '<h1>' + coop.title + '</h1><p>Please refer to our <a href="' + coop.RITJobZoneGuidelink + '" target="_blank">Co-op Guide</a>!';
                $.each(coop.enrollmentInformationContent, function(i, info) {
                    html = html + '<h2>' + info.title + '</h2><p>' + info.description + '</p>';
                });
                $('#popup').append(html);
                // Show overlay
                $('#popup').popup('show');
            });
    }


    /**************************** CONTACT FORM ****************************/
    // Query Contact Form  
    xhr('get', 'html', { path : '/contactForm/' }).done(function(results) {
        console.log("Getting contact form...");
        $('#contact').append(results);
    });

    /**************************** FOOTER ****************************/

    // Query "Footer" data 
    xhr('get', 'json', { path : '/footer/' }).done(function(json) {
        processFooter(json);
    });

    function processFooter(data) {
        console.log("Processing footer...");
        console.log(data);

        var social = data.social;
        var quickLinks = data.quickLinks;
        var copyright = data.copyright;

        $('#social').append('<p>' + social.title + '</p><p>' + social.facebook + '</p><p>' + social.tweet + ' <a href="' + social.twitter + '">' + social.by + '</a></p>');

        // Add each quick link
        $.each(quickLinks, function(i, quicklink) {
            $('#quicklinks').append('<a href="' + quicklink.href + '" target="_blank">' + quicklink.title + '</a><br>');
        });


        $('footer').append(copyright.html);
    }

    /**************************** MENU ****************************/

    $('.menu-btn').each(function(index, element) {
        $(this).on('click', function() {
            var name = $(this).text();
            if (name == "Degrees") {
                wipeAnimation.seek(1);
                wipeAnimation.play(1);
            } else if (name == "People") {
                wipeAnimation.seek(2);
            } else if (name == "Research") {
                wipeAnimation.seek(3);
            } else if (name == "Employment") {
                //wipeAnimation.seek(4);
                //controller.scrollTo('#start');
             //   controller.scrollTo('#employmentAnchor')
            //scene.progress(0.56);
                
                //wipeAnimation.play(4);

                //alert(scene.progress());
                //scene.progress(0.5);
                //controller.scrollTo('#start');
                
                //alert('current pos: ' + controller.scrollPosMethod());
                //wipeAnimation.play(4);
                //wipeAnimation.start();
                //wipeAnimation.play(4);
            } else if (name == "Resources") {
                wipeAnimation.seek(5);
            }
        });
    })

    $('.menu').on('click', function() {
        $('nav').show();
        $(this).hide();
        $('.close').show();
    })

    $('.close').on('click', function() {
        $('nav').hide();
        $(this).hide();
        $('.menu').show();
    })


    /**************************** SCROLLING ****************************/

    // Define movement of panels
    var wipeAnimation = new TimelineMax()
        .to("#slideContainer", 1,   {x: "-11.11%"})    // to Degrees
        .to("#slideContainer", 1,   {x: "-22.22%"})    // to People
        .to("#slideContainer", 1,   {x: "-33.33%"})    // to Research
        .to("#slideContainer", 1,   {x: "-44.44%"})    // to Employment
        .to("#slideContainer", 1,   {x: "-55.56%"})    // to Employment2
        .to("#slideContainer", 1,   {x: "-66.67%"})    // to Employment3
        .to("#slideContainer", 1,   {x: "-77.78%"});   // to Resources

    // Create scene to pin and link animation
    var scene = new ScrollMagic.Scene({
            triggerElement: "#pinContainer",
            triggerHook: "onLeave",
            duration: "600%"  // slows down scrolling
        })
        .setPin("#pinContainer")
        .setTween(wipeAnimation)
        //.addIndicators() 
        .addTo(controller);

    scene.on("progress", function (event) {
        console.log("Scene progress changed to " + event.progress);
    });

});


