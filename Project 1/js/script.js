// Redirect outdated browsers
if (!document.getElementById) {
    window.location = "legacy.html";
}

// Set console.log as an empty function in case IE doesn't recognize it (happened once)
if (!window.console) {
    console = {log: function() {}};
}

// When DOM loads, proceed with code
window.onload = function() {
    // Save info abt whether browser is IE 
    var isIE = ((navigator.userAgent.indexOf("MSIE") != -1) && (navigator.userAgent.indexOf("Opera") == -1)); 

    // Get name form & set its onsubmit event listener to handle validation
    var nameForm = document.getElementsByTagName('form')[0];

    // Get selections form & set its onsubmit event listener to handle validation
    var form = document.getElementsByTagName('form')[1];
    if (form.addEventListener) {
        form.addEventListener("submit", function(e) {
            validate(e);
            return false; // firefox
        }); // Modern browsers  
    } else if (form.attachEvent) {
        form.attachEvent('onsubmit', function(e) {
            validate(e);
            return false; // firefox
        }); // IE           
    }

    // Initialize other globals
    var errorMsg = document.getElementById('errorMsg');
    var p = document.getElementById('name');
    var selectsLive = form.getElementsByTagName('select');
    var activeSelectId = 1; // keeps track of <select> elements
    var divsLive = document.getElementsByTagName('div');
    var optionTexts;  // holds text for questions & choices
    var currNode;
    var guess;
    var answer;

    // Get reset button & set its onclick event listener to handle further reset functionality
    resetButton = document.getElementById("resetBtn");
    resetButton.onclick = reset;

    // Get "play again" button & set its onclick event listener to handle generating a new answer
    playAgainButton = document.getElementById("playAgain");
    playAgainButton.onclick = function () {
        form.reset();
        reset(); 
        answer = [];
        generateRandomAnswer(optionTexts[1]);
        console.log(answer);
    }

    // If browser supports XMLHttpRequest,
    if (window.XMLHttpRequest) {
        console.log("Sending AJAX request...");
        var xmlhttp = new XMLHttpRequest();
        var dataUrl = 'data/data2.json';
        //var dataUrl = 'data/data.json'; // this will also load
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                optionTexts = JSON.parse(this.responseText);
                begin();
            }
        }
        xmlhttp.open("GET", dataUrl);
        xmlhttp.send();
    } else {
        // If the browser does not support XMLHttpRequest (all required browsers do, so this part is not needed)
        // https://msdn.microsoft.com/en-us/library/ms537505(v=vs.85).aspx
        console.log("Browser does not support XMLHttpRequest, loading hard-coded json");
        var json = '{"1": {"square 1:": null,"blue": {"square 2:": null,"lightsteelblue": {"square 3:": null,"lightyellow": null,"lightcoral": null},"deepskyblue": {"square 3:": null,"midnightblue": null,"mediumpurple": null},"lightskyblue": {"square 3:": null,"palegoldenrod": null,"oldlace": null}},"red": {"square 2:": null,"pink": {"square 3:": null,"sandybrown": null,"sienna": null},"palevioletred": {"square 3:": null,"wheat": null,"yellow": null},"magenta": {"square 3:": null,"teal": null,"violet": null}},"green": {"square 2:": null,"lime": {"square 3:": null,"mistyrose": null,"orange": null},"mediumspringgreen": {"square 3:": null,"paleturquoise": null,"rosybrown": null},"olivedrab": {"square 3:": null,"salmon": null,"silver": null}}}}';
        optionTexts = JSON.parse(json);
        begin();
    }

    function begin() {
        // Set optionTexts node 'pointer' to first node
        currNode = optionTexts[1];
        // Generate random answer
        answer = [];
        generateRandomAnswer(optionTexts[1]);
        console.log(answer);
        // Get keys for first drop down menu
        var keys = getKeys(currNode);
        // Create the menu
        createDropMenu(keys, activeSelectId);
        // Get user choices if saved in storage
        getUserChoices();
    }

    // Build array of 3 random colors that will be compared against user's choices
    function generateRandomAnswer(node) {
        // If empty node, quit navigating beyond this node
        if (node === null || node === undefined) {
            console.log('node was undefined...');
            return;
        }

        var randomColor = getRandomColor();
        answer.push(randomColor);
        if (answer.length < 3) {
            generateRandomAnswer(node[randomColor]);
        }
        
        function getRandomColor() {
            var keys = getKeys(node);
            var prop = keys[Math.floor(Math.random() * (keys.length - 1)) + 1];
            if (!prop.match(/square \d+/)) {
                return prop;
            }
            
            getRandomColor();
        }
    } 

    // Utility function to get an object's upper level keys
    function getKeys(obj) {
        var keys = []
        for (key in obj) {
            keys.push(key);
        }
        return keys;
    }

    // Creates all of the dropdowns
    function createDropMenu(texts, id) {
        form.appendChild(playAgainButton);
        var select = document.createElement("select");
        select.name = id;
        select.setAttribute('id', 'select' + id);
        if (select.addEventListener) {
            select.addEventListener("change", respondToSelection);
        } else {
            select.attachEvent("onchange", respondToSelection);
        }

        // Event handler
        function respondToSelection() {
            var target;
            if (isIE) {
                target = window.event.target || window.event.srcElement;
            } else {
                target = this;
            }

            colorBall(target);
            getNext(target);

            // If user randomly chooses earlier selection, 
            // remove border colors from wrapper divs 
            if (select.name !== ((getKeys(optionTexts[1])).length - 1)) {
                removeAnswers();
            }
        }

        // Create question <p>
        var question = document.createElement('label');
        question.setAttribute('for', select.getAttribute('id'));
        question.appendChild(document.createTextNode(texts[0]));
        form.insertBefore(question, resetButton);

        // Add blank choice for display purposes
        select.appendChild(document.createElement("option"));

        // Add actual choices
        for (var i = 1; i < texts.length; i++) {
            var option = document.createElement("option");
            var optionText = document.createTextNode(texts[i]);
            //option.setAttribute('value', optionText);
            option.appendChild(optionText);
            select.appendChild(option);
        }
        
        // Append the new dropdown menu to the form with fade animation
        select.style.opacity = 0;
        form.insertBefore(select, resetButton);
        fadeIn(select);
    }

    // Colors each square
    function colorBall(selectElement) {
        var color = selectElement.options[selectElement.selectedIndex].text;  // accommodates IE
        var whichSquare = selectElement.name;

        for (var i = 0; i < divsLive.length; i++) {
            if (divsLive[i].getAttribute('title') == whichSquare) {
                divsLive[i].style.background = color;
            }
        }

        // Clear any squares after current square
        for (var i = 0; i < divsLive.length; i++) {
            if (divsLive[i].title > whichSquare) {
                divsLive[i].style.background = "#F9FAFC";
            }
        }
    }

    // Search for next options based on the option selected
    function getNext(select) {
        // Clear any <select>s after current <select>
        var selects = form.getElementsByTagName('select');
        for (var i = selects.length-1; i >= 0; i--) {
            if (selects[i].name > select.name) {
                var eleToRemove = selects[i];
                form.removeChild(eleToRemove.previousSibling); // removes label
                form.removeChild(eleToRemove);
            }
        }

        // Search data structure for matching selection, beginning with first node
        navigate(optionTexts[1]);

        function navigate(node) {
            for (var key in node) {
                if (node[key] !== null && key == select.options[select.selectedIndex].text) {
                    currNode = node[key];
                    activeSelectId = Number(select.name) + 1;
                    createDropMenu(getKeys(currNode), activeSelectId);
                    return;
                } else {
                    navigate(node[key]);
                }
            }
        }
    }

    // Utility function, fade in passed element
    function fadeIn(element) {
        // https://css-tricks.com/css-transparency-settings-for-all-broswers/
        var opacity = 0.1;
        // Start fade
        var timer = setInterval(function() {
            // Once opacity hits full, stop fading
            if (opacity >= 1) {
                clearInterval(timer);
            }
            // Set new opacity
            element.style.opacity = opacity;
            element.style.filter = "progid:DXImageTransform.Microsoft.Alpha(Opacity=" + (opacity*100) + ")"; // IE 8
            element.style.filter = "alpha(opacity=" + (opacity*100) + ")"; // IE 7
            // Increment opacity for next loop
            opacity += 0.1;
        }, 50); 
    }

    // Get user choices from storage or cookies
    function getUserChoices() {
        var index = 0;
        var choice;
        var oIndex;
        var selectElement;
        if (window.localStorage) {
            while (true) {
                choice = window.localStorage.getItem(index+1);
                if (!choice) {
                    break;
                } else {
                    // If a choice has been saved, proceed
                    console.log('getting user choices from local storage...');
                    selectElement = selectsLive[index];
                    oIndex = 1;
                    while (selectElement[oIndex]) {
                        if (selectElement[oIndex].text == choice) {
                            break;
                        }
                        oIndex += 1;
                    }

                    selectElement.options.selectedIndex = oIndex;
                    colorBall(selectElement);
                    getNext(selectElement);
                    index += 1;
                }
            }
        } else {
            // If browser doesn't support window.localStorage, check cookies for choices
            if (document.cookie.indexOf('1=') != -1) {
                while (true) {
                    var re = new RegExp('' + (index+1) + '=([\\w\\s]+)');
                    choice = document.cookie.match(re);
                    // Exit if no saved choices were found
                    if (!choice) {
                        break;
                    } else {
                        console.log('getting user choices from cookies...');
                        choice = choice[1]; // grab first matching group
                        // If a choice has been saved, proceed
                        selectElement = selectsLive[index];
                        oIndex = 1;
                        while (selectElement[oIndex]) {
                            if (selectElement[oIndex].text == choice) {
                                break;
                            }
                            oIndex += 1;
                        }

                        selectElement.options.selectedIndex = oIndex;
                        colorBall(selectElement);
                        getNext(selectElement);
                        index += 1;
                    }
                }
            }
        }
    }

    // Reset form with selections
    function reset() {
        // Hide description for returning user saved choices
        note.style.display = "none";

        // Hide invalid message if it is showing
        if (errorMsg.style.display !== "none") {
            errorMsg.style.display = "none";
        }

        // Remove saved choices from local storage and cookies
        for (var i = 0; i < selectsLive.length; i++) {
            if (window.localStorage) {
                console.log("removing user choices from local storage");
                window.localStorage.removeItem(selectsLive[i].name);
            } else {
                console.log("removing user choices from cookies");
                var lastyear = new Date();
                lastyear.setFullYear(lastyear.getFullYear()-1);
                document.cookie = (i+1) + "=" + selectsLive[i].value + "; expires=" + lastyear.toGMTString() + "; path=/";
            }
        }

        // Clear squares
        for (i = 0; i < divsLive.length; i++) {
            if (divsLive[i].title) {
                divsLive[i].style.backgroundColor = "#F9FAFC";
            }
        }

        // Clear answers
        removeAnswers();

        // Clear selects
        var selects = form.getElementsByTagName('select');
        for (var i = selects.length-1; i >= 1; i--) {
            var eleToRemove = selects[i];
            form.removeChild(eleToRemove.previousSibling); // removes label
            form.removeChild(eleToRemove);
        }
    }

    // Removes answers, the red/green outlines around the colored boxes
    function removeAnswers() {
        for (var i = 0; i < divsLive.length; i++) {
            var thisDiv;
            if (divsLive[i].className == 'wrapper') {
                thisDiv = divsLive[i];
                thisDiv.style.border = "0px solid black"; 
            }
        }
    }

    var greeting = "Welcome to the color lottery";
    var returnGreeting = "Welcome back to the color lottery";
    var note = document.getElementById("description");
    var name;
    getNameFromStorage();

    // Get name from storage or cookies
    function getNameFromStorage() {
        // If browser supports localStorage, retrieve name & show
        if (window.localStorage) {
            name = window.localStorage.getItem('name');
            if (name) {
                showName(true); // returned user
            } else {
                // Otherwise prompt for name
                promptName(); // new user
            }
        } else {
            // If browser doesn't support window.localStorage, check cookies for name
            if (document.cookie.indexOf('nameCookie') != -1) {
                name = document.cookie.match(/nameCookie=([\w\s]+)/)[1];
                showName(true); // returned user
            } else {
                // Otherwise prompt for name
                promptName(); // new user
            }
        }
    }

    // Show name string
    function showName(returned) {
        // Remove everything from the name element
        while (p.firstChild) {
            p.removeChild(p.firstChild);
        }

        // Show name depending on if new or returning user
        if (returned) {
            // Show returned user greeting
            p.appendChild(document.createTextNode(returnGreeting + ", " + name + "! "));
            // If localStorage contains name & saved selection info, show description
            if (localStorage.length >= 4) {
                note.style.display = "block";
            }
        } else {
            // Show new user greeting
            p.appendChild(document.createTextNode(greeting + ", " + name + "! "));
            // Don't show description
            note.style.display = "none";
        }

        // Give user option to change name again
        var a = document.createElement('a');
        a.appendChild(document.createTextNode('Not you?'));
        a.setAttribute('href', '#');
        p.appendChild(a);
        // If user chooses to change name,
        a.onclick = function() {
            // Remove name from local storage and cookies (not sure if did cookies right)
            if (window.localStorage) { 
                console.log("removing name from local storage");
                window.localStorage.removeItem('name');
            } else {
                if (document.cookie.indexOf('nameCookie') != -1) {
                    console.log("removing name cookie...");
                    var lastyear = new Date();
                    lastyear.setFullYear(lastyear.getFullYear()-1);
                    document.cookie = "nameCookie=" + name + "; expires=" + lastyear.toGMTString() + "; path=/";
                }
            }

            // Show form prompting user for name
            promptName();

            // Stop browser from redirecting to #
            return false;
        };
    }

    // Ask user for name
    function promptName() {
        // Remove everything from the name element 
        while (p.firstChild) {
            p.removeChild(p.firstChild);
        }
        // Set name element to new user greeting
        p.appendChild(document.createTextNode(greeting + "!"));

        // Reset game for new user
        reset();
        // Display form prompting user for name
        nameForm.style.display = "inline";
        // Attach validation event handler to form onsubmit
        if (nameForm.addEventListener) {
            nameForm.addEventListener("submit", function(e) {
                validateName(e);
                return false;
            }); // Modern browsers  
        } else if (nameForm.attachEvent) {
            nameForm.attachEvent('onsubmit', function(e) {
                validateName(e);
                return false;
            }); // IE           
        }
    }

    // Validate user name input
    function validateName(e) { 
        // Prevent form from being submitted (considers IE)
        e.preventDefault ? e.preventDefault() : e.returnValue = false;
        console.log("validating name...");

        var nameInput = nameForm.elements["nameinput"];
        var nameError = document.getElementById("userError");

        // Trim white spaces from beginning & end of input
        name = nameInput.value.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
        // If name is valid (not an empty string, and consists of only letters)
        if (name.length !== 0 && /^[a-zA-Z ]+$/.test(name)) {
            // Remove error msg if assigned
            nameError.style.display = "none";
            // Remove invalid css if assigned
            nameInput.className = "";
             // If valid, save name
            saveName();
            // Show name
            showName();
            // Remove name form
            nameForm.style.display = "none";
        } else {
            nameInput.className = "invalid";
            nameError.style.display = "block";
            console.log('name is not valid');
        }
    }

    // Save user name to storage or cookie
    function saveName() {
        if (window.localStorage) {
            window.localStorage.setItem('name', name);
            console.log("saved " + window.localStorage.getItem('name') + " to localStorage");
        } else {
            var nextyear = new Date();
            nextyear.setFullYear(nextyear.getFullYear()+1);
            document.cookie = "nameCookie=" + name + "; expires=" + nextyear.toGMTString() + "; path=/";
            console.log("saved " + name + " to cookie");
        }
    }

    // On click of "Check" button, this callback gets called
    function validate(e) {
        // Prevent form from being submitted (considers IE)
        e.preventDefault ? e.preventDefault() : e.returnValue = false;

        console.log('validating form...');
        // Make sure no selection is empty.
        // We only need to check the last selection, 
        // as it only appears after other selections have been filled in
        console.log('value: ' + typeof(selectsLive[selectsLive.length - 1].options[selectsLive[selectsLive.length - 1].selectedIndex].text));
        var lastValue = selectsLive[selectsLive.length - 1].options[selectsLive[selectsLive.length - 1].selectedIndex].text;
        if (lastValue !== "") { 
            // Hide invalid message if it is showing
            if (errorMsg.style.display !== "none") {
                errorMsg.style.display = "none";
            }
            // Save answers
            saveUserChoices();
            // Check whether user guessed correctly
            check();
        } else {
            // Show invalid msg if it isn't showing already
            if (errorMsg.style.display == "none") {
                errorMsg.style.display = "block";
                console.log("Form is not valid");
            }
        }
    }

    // Check user choices against randomly generated answer
    function check() {
        for (var i = 0; i < guess.length; i++) {
            // Get the right div (IE7 compatible)
            var thisDiv;
            for (var j = 0; j < divsLive.length; j++) {
                if (divsLive[j].getAttribute('title') == (i+1)) {
                    thisDiv = divsLive[j];
                    break;
                }
            }

            if (guess[i] == answer[i]) {
                thisDiv.parentNode.style.border = "2px solid green";
            } else {
                thisDiv.parentNode.style.border = "2px solid red";
            }
        }
    }

    // Save user choices to localstorage or cookies
    function saveUserChoices() {
        guess = [];
        // Save choice for each select element
        for (var i = 0; i < selectsLive.length; i++) {
            if (window.localStorage) {
                console.log("saving user choices to local storage");
                window.localStorage.setItem((i+1), selectsLive[i].options[selectsLive[i].selectedIndex].text);
            } else {
                var nextyear = new Date();
                nextyear.setFullYear(nextyear.getFullYear()+1);
                var cookieStr = (i+1) + "=" + selectsLive[i].options[selectsLive[i].selectedIndex].text + "; expires=" + nextyear.toUTCString() + "; path=/";
                console.log("saving user choices to cookies: " + cookieStr);
                document.cookie = cookieStr;
            }

            // also populate guess array
            guess.push(selectsLive[i].options[selectsLive[i].selectedIndex].text);
        }
    }

};



