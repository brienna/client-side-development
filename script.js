if (!document.getElementById) {
    window.location = "legacy.html";
}

window.onload = function() {

    // Access form 
    var form = document.getElementsByTagName('form')[0];
    var p = document.getElementById('name');
    var selectsLive = form.getElementsByTagName('select');
    var optionTexts;  // holds text for questions & choices
    var currNode;
    var activeSelectId = 1; // keeps track of <select> elements

    ///////////////////////////////// LOAD DATA /////////////////////////////////

    // Create the HTTP object
    var xmlhttp = new XMLHttpRequest();
    var dataUrl = 'data.json';
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            optionTexts = JSON.parse(this.responseText);
            // Set optionTexts node 'pointer' to first node
            currNode = optionTexts[1];
            createDropMenu(Object.keys(currNode), activeSelectId);
            getUserChoices();
        }
    };
    xmlhttp.open("GET", dataUrl);
    xmlhttp.send();


    ///////////////////////////////// SHOW GREETING /////////////////////////////////
    var greeting = "Welcome to the geometric rains";
    var returnGreeting = "Welcome back to the geometric rains";
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

    function promptName() {
        p.firstChild.nodeValue = greeting + "! Who are you? ";
        var nameInput = document.createElement('input');
        var nameSubmit = document.createElement('button');
        nameSubmit.appendChild(document.createTextNode('Submit'));
        p.appendChild(nameInput);
        p.appendChild(nameSubmit);
        nameSubmit.addEventListener('click', function() {
            // Trim white spaces from beginning & end of input
            name = nameInput.value.replace(/^\s\s*/, '').replace(/\s\s*$/, '');
            // Show name if it isn't a empty string
            if (name.length !== 0) {
                p.removeChild(nameSubmit);
                p.removeChild(nameInput);
                saveName();
                showName(false);
            }
        });
    }

    function showName(returned) {
        // Show name
        if (returned) {
            p.firstChild.nodeValue = returnGreeting + ", " + name + "! ";
        } else {
            p.firstChild.nodeValue = greeting + ", " + name + "! ";
        }
        // Give user option to change name again
        var a = document.createElement('a');
        a.appendChild(document.createTextNode('(Not you?)'));
        a.setAttribute('href', '#');
        a.onclick = function() {
            p.removeChild(a);
            promptName();
            return false;  // so browser doesn't redirect? 
        };
        p.appendChild(a);
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
    
    ///////////////////////////////// SHOW MENUS /////////////////////////////////

    // Creates all of the dropdowns
    function createDropMenu(texts, id) {
        var select = document.createElement("select");
        select.name = id;
        select.addEventListener('change', function(e) { 
            getNext(this);
            // If it was last menu && actual choice was picked, save user choices
            if (select.name == (Object.keys(optionTexts[1])).length && select.value != "") {
                saveUserChoices();
                form.style.borderBottom = "2px solid #000000";
            } else {
                form.style.border = 'none';
            }
        });
        
        // Create question <p>
        var question = document.createElement('p');
        question.setAttribute('class', 'question');
        question.appendChild(document.createTextNode(texts[0]));
        form.append(question);

        // Add blank choice for display purposes
        select.appendChild(document.createElement("option"));

        // Add actual choices
        for (var i = 1; i < texts.length; i++) {
            var option = document.createElement("option");
            var optionText = document.createTextNode(texts[i]);
            option.value = texts[i];
            option.appendChild(optionText);
            select.appendChild(option);
        }
        
        // Append the new dropdown menu to the form with animation
        select.style.opacity = 0;
        form.appendChild(select);
        form.appendChild(document.createElement('br'));
        fadeIn(select);
    }

    function saveUserChoices() {
        console.log('saving user choices...');
        // Save choice for each select element
        for (var i = 0; i < selectsLive.length; i++) {
            if (window.localStorage) {
                window.localStorage.setItem((i+1), selectsLive[i].value);
            } else {
                var nextyear = new Date();
                nextyear.setFullYear(nextyear.getFullYear()+1);
                document.cookie = (i+1) + "=" + selectsLive[i].value + "; expires=" + nextyear.toGMTString() + "; path=/";
                console.log("saved " + selectsLive[i].value + " to cookie");
            }
        }
    }

    function getUserChoices() {
        // Check if user choices exist
        var selectName = 1;
        if (window.localStorage) {
            while (true) {
                var choice = window.localStorage.getItem(selectName);
                if (!choice) {
                    break;
                } else {
                    // If a choice has been saved, proceed
                    var selectElement = selectsLive.namedItem(selectName);
                    selectElement.value = choice;
                    getNext(selectElement);
                    selectName += 1;    
                }
            }
        } else {
            // If browser doesn't support window.localStorage, check cookies for choices
            if (document.cookie.indexOf('1=') != -1) {
                while (true) {
                    var re = new RegExp(selectName + '=([\\w\\s]+)');
                    var choice = document.cookie.match(re);
                    if (!choice) {
                        break;
                    } else {
                        var selectElement = selectsLive.namedItem(selectName);
                        selectElement.value = choice[1];
                        getNext(selectElement);
                        selectName += 1;
                    }
                }
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
                if (node[key] !== null && key == select.value) {
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
};
