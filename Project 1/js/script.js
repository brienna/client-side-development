if (!document.getElementById) {
    window.location = "legacy.html";
}

window.onload = function() {

    // Save info abt whether browser is IE 
    var isIE = ((navigator.userAgent.indexOf("MSIE") != -1) && (navigator.userAgent.indexOf("Opera") == -1)); 

    // Get form & set its onsubmit event listener to handle validation
    var form = document.getElementsByTagName('form')[0];
    if (form.addEventListener) {
        form.addEventListener("submit", function() { // Modern browsers
            validate(event)
        });  
    } else if (form.attachEvent) {
        form.attachEvent('onsubmit', function() {  // IE
            validate(event);
        });            
    }

    // Get reset button & set its onclick event listener to handle further reset functionality
    resetButton = document.getElementById("resetBtn");
    resetButton.onclick = reset;

    var p = document.getElementById('name');
    var selectsLive = form.getElementsByTagName('select');
    var divsLive = document.getElementsByTagName('div');
    var optionTexts;  // holds text for questions & choices
    var currNode;
    var activeSelectId = 1; // keeps track of <select> elements
    var answer;
    var guess;

    var playAgainButtonTxt = document.createTextNode('Generate new answer');
    var playAgainButton = document.createElement('button');
    playAgainButton.appendChild(playAgainButtonTxt);
    playAgainButton.style.marginLeft = '10px';
    playAgainButton.onclick = function () {
        reset();
        answer = [];
        generateRandomAnswer(optionTexts[1]);
        console.log(answer);
        return false;
    }

    ///////////////////////////////// LOAD DATA /////////////////////////////////

    if (isIE) {
        // NOTE: Couldn't make AJAX work with IE, because of multiple errors...? https://stackoverflow.com/questions/48979128/cant-send-xmlhttprequest-xdomainrequest-in-ie8-fails-on-xhr-send
        var json = '{"1": {"square 1:": null,"blue": {"square 2:": null,"lightsteelblue": {"square 3:": null,"lightyellow": null,"lightcoral": null},"deepskyblue": {"square 3:": null,"midnightblue": null,"mediumpurple": null},"lightskyblue": {"square 3:": null,"palegoldenrod": null,"oldlace": null}},"red": {"square 2:": null,"pink": {"square 3:": null,"sandybrown": null,"sienna": null},"palevioletred": {"square 3:": null,"wheat": null,"yellow": null},"magenta": {"square 3:": null,"teal": null,"violet": null}},"green": {"square 2:": null,"lime": {"square 3:": null,"mistyrose": null,"orange": null},"mediumspringgreen": {"square 3:": null,"paleturquoise": null,"rosybrown": null},"olivedrab": {"square 3:": null,"salmon": null,"silver": null}}}}';
        optionTexts = JSON.parse(json);
        begin();
    } else {
        // Create the HTTP object
        var xmlhttp = new XMLHttpRequest();
        var dataUrl = 'data/data2.json';
        //var dataUrl = 'data/data.json'; // this will also work but won't fit in the scheme of the game
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                optionTexts = JSON.parse(this.responseText);
                begin();
            }
        };
        xmlhttp.open("GET", dataUrl);
        xmlhttp.send();
    }

    function begin() {
        // Set optionTexts node 'pointer' to first node
        currNode = optionTexts[1];
        // Generate random answer
        answer = [];
        generateRandomAnswer(optionTexts[1]);
        console.log(answer);
        // Create first drop down menu
        createDropMenu(Object.keys(currNode), activeSelectId);
        // Get user choices if saved in storage
        getUserChoices();
    }

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
            var keys = Object.keys(node);
            var prop = keys[Math.floor(Math.random() * (keys.length - 1)) + 1];
            if (!prop.match(/square \d+/)) {
                return prop;
            }
            
            getRandomColor();
        }
    } 

    ///////////////////////////////// SHOW MENUS /////////////////////////////////

    // Creates all of the dropdowns
    function createDropMenu(texts, id) {
        form.appendChild(playAgainButton);
        var select = document.createElement("select");
        select.name = id;
        if (select.addEventListener) {
            select.addEventListener("change", respondToSelection);
        } else {
            select.attachEvent("onchange", respondToSelection);
        }

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
            if (select.name !== ((Object.keys(optionTexts[1])).length - 1)) {
                removeAnswers();
            }
        }

        // Create question <p>
        var question = document.createElement('p');
        question.setAttribute('class', 'question');
        question.appendChild(document.createTextNode(texts[0]));
        form.insertBefore(question, resetButton);

        // Add blank choice for display purposes
        select.appendChild(document.createElement("option"));

        // Add actual choices
        for (var i = 1; i < texts.length; i++) {
            var option = document.createElement("option");
            var optionText = document.createTextNode(texts[i]);
            option.value = optionText;
            option.appendChild(optionText);
            select.appendChild(option);
        }
        
        // Append the new dropdown menu to the form with animation
        if (!isIE) {
            select.style.opacity = 0;
        }
        form.insertBefore(select, resetButton);
        form.insertBefore(document.createElement('br'), resetButton);
        if (!isIE) {
            fadeIn(select);
        }
    }

    function colorBall(selectElement) {
        var color = selectElement.options[selectElement.selectedIndex].text;  // accommodates IE
        var whichSquare = selectElement.name;
        document.querySelector('[title="'+whichSquare+'"]').style.background = color;

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
        var selects = form.querySelectorAll('select');
        for (var i = 0; i < selects.length; i++) {
            if (selects[i].name > select.name) {
                var eleToRemove = selectsLive.namedItem(selects[i].name);
                form.removeChild(eleToRemove.previousSibling);
                form.removeChild(eleToRemove.nextSibling);
                form.removeChild(selectsLive.namedItem(selects[i].name));
            }
        }

        // Search data structure for matching selection, beginning with first node
        navigate(optionTexts[1]);

        function navigate(node) {
            for (var key in node) {
                if (node[key] !== null && key == select.options[select.selectedIndex].text) {
                    currNode = node[key];
                    activeSelectId = Number(select.name) + 1;
                    createDropMenu(Object.keys(currNode), activeSelectId);
                    return;
                } else {
                    navigate(node[key]);
                }
            }
        }
    }

    function fadeIn(e) {
        // Incremental animation
        var tick = function() {
            e.style.opacity = +e.style.opacity + 0.05;
                if (+e.style.opacity < 1) {
                    // If the browser supports window.requestAnimationFrame
                    if (window.requestAnimationFrame) {
                        window.requestAnimationFrame(tick);
                    } else {
                        setTimeout(tick, 16);
                    }
                }
        };
        // Begin animation
        tick();
    }

    function getUserChoices() {
        // Check if user choices exist in localstorage or cookies
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
                    var re = new RegExp((index+1) + '=([\\w\\s]+)');
                    choice = document.cookie.match(re);
                    if (!choice) {
                        break;
                    } else {
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

    function reset() {
        // Remove saved choices from local storage and cookies
        for (var i = 0; i < selectsLive.length; i++) {
            if (window.localStorage) {
                window.localStorage.removeItem(selectsLive[i].name);
            } else {
                //var lastyear = new Date();
                //last.setFullYear(lastyear.getFullYear()-1);
                document.cookie = (i+1) + "=" + selectsLive[i].value + "; expires=" + new Date() + "; path=/";
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
        var selects = form.querySelectorAll('select');
        for (i = 1; i < selects.length; i++) {
            var eleToRemove = selectsLive.namedItem(selects[i].name);
            form.removeChild(eleToRemove.previousSibling);
            form.removeChild(eleToRemove.nextSibling);
            form.removeChild(selectsLive.namedItem(selects[i].name));
        }
    }

    // Removes answers, the red/green outlines around the colored boxes
    function removeAnswers() {
        var wrappers = document.getElementsByClassName('wrapper');
        for (i = 0; i < wrappers.length; i++) {
            if (wrappers[i].style.border !== "") {
                wrappers[i].style.border = "";
            }
        }
    }

    ///////////////////////////////// SHOW GREETING /////////////////////////////////

    var greeting = "Welcome to the color lottery";
    var returnGreeting = "Welcome back to the color lottery";
    var name;
    getNameFromStorage();

    function getNameFromStorage() {
        // If browser supports localStorage, retrieve name & show
        if (window.localStorage) {
            name = window.localStorage.getItem('name');
            if (name) {
                showName(true);
            } else {
                // Otherwise prompt for name
                promptName();
            }
        } else {
            // If browser doesn't support window.localStorage, check cookies for name
            if (document.cookie.indexOf('nameCookie') != -1) {
                name = document.cookie.match(/nameCookie=([\w\s]+)/)[1];
                showName(true);
            } else {
                // Otherwise prompt for name
                promptName();
            }
        }
    }

    function showName(returned) {
        var note = document.getElementById("description");
        // Show name
        if (returned) {
            p.firstChild.nodeValue = returnGreeting + ", " + name + "! " ;
            if (localStorage.length >= 4) {
                note.style.display = "block";
            }
        } else {
            p.firstChild.nodeValue = greeting + ", " + name + "! ";
            note.style.display = "none";
        }
        // Give user option to change name again
        var a = document.createElement('a');
        a.appendChild(document.createTextNode('(Not you?)'));
        a.href = '#';
        a.onclick = function() {
            p.removeChild(a);
            promptName();
            return false;  // so browser doesn't redirect? 
        };
        p.appendChild(a);
    }

    function promptName() {
        p.firstChild.nodeValue = greeting + "! Who are you? ";
        var nameInput = document.createElement('input');
        var nameBtn = document.createElement('button');
        nameBtn.appendChild(document.createTextNode('Submit'));
        p.appendChild(nameInput);
        p.appendChild(nameBtn);

        if (nameBtn.addEventListener) {
            nameBtn.addEventListener("click", submitName);
        } else {
            nameBtn.attachEvent("onclick", submitName);
        }

        function submitName() {
            reset();

            // Trim white spaces from beginning & end of input
            name = nameInput.value.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
            // Show name if it isn't a empty string
            if (name.length !== 0) {
                p.removeChild(nameBtn);
                p.removeChild(nameInput);
                saveName();
                showName(false);
            }
        }
    }

    function saveName() {
        // Save to localStorage or cookie
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

    //////////////////// FORM SUBMISSION ////////////////////////

    // On click of "Check" button, this callback gets called
    function validate(evt) {
        // Prevent form from being submitted
        evt.preventDefault();

        // Make sure no selection is empty.
        // We only need to check the last selection, 
        // as it only appears after other selections have been filled in
        if (selectsLive[selectsLive.length - 1].value !== "") {
            console.log("Form is valid");
            // Save answers
            saveUserChoices();
            // Check whether user guessed correctly
            check();
        } else {
            console.log("Form is not valid");
        }
    }

    // Check user choices against randomly generated answer
    function check() {
        for (var i = 0; i < guess.length; i++) {
            if (guess[i] == answer[i]) {
                document.querySelector('[title="'+(i+1)+'"]').parentNode.style.border = "2px solid green";
            } else {
                document.querySelector('[title="'+(i+1)+'"]').parentNode.style.border = "2px solid red";
            }
        }
    }

    // Save user choices to localstorage or cookies
    function saveUserChoices() {
        guess = [];
        // Save choice for each select element
        for (var i = 0; i < selectsLive.length; i++) {
            if (window.localStorage) {
                window.localStorage.setItem((i+1), selectsLive[i].options[selectsLive[i].selectedIndex].text);
            } else {
                var nextyear = new Date();
                nextyear.setFullYear(nextyear.getFullYear()+1);
                var cookieStr = (i+1) + "=" + selectsLive[i].options[selectsLive[i].selectedIndex].text + "; expires=" + nextyear.toGMTString() + "; path=/";
                document.cookie = cookieStr;
            }

            // also populate guess array
            guess.push(selectsLive[i].options[selectsLive[i].selectedIndex].text);
        }
    }

};



