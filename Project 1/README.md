
# Project 1: Interactive form elements

Web page that contains a form using a select pull-down element. Once the user selects the desired option, the script will dynamically create another pull-down element whose contents will depend upon the visitor's first choice. Once the user has made 3 different choices, create a new text node and dynamically print out the user's choices to the scren.

The first option/select menu must NOT be hard coded in html. Use one function to create all of the dropdowns, the content of the select/option should be dependent upon what information is sent to the function. 

## Requirements

- Dynamically creating form elements depending upon the answer of the last question
    - The form elements for the questionnaire MUST be <select><option> elements
- At least 2 choices for each selection
- At least a depth of 3 different questions 
- Once the user has made their selections, (at least) create a new node to print out their final choices to the screen 
- Give the user the option to start over at any time
- A form on some page getting the users information (validation)
- The use of cookies & localStorage - project must work in all browsers (Mac - Firefox, Chrome, Safari; PC - IE 7-11, Firefox, Chrome, and Safari).
- Re-direct all browsers that this doesn't work on to another page, asking them to download a browser that would work
- Looks!! (professional looking, quality graphics, etc)

## Programming requirements

- Dynamic creation of at least 3 sets of <select><option> value... nodes (tags)
- Constructor method that creates the next select menu from the last choice
- Use of the following commands (among many others):
    - createElement, setAttribute, appendChild, getElementById, getElementsByTagName, createTextNode, nodeValue to name a few...
- Do NOT use visibility to show/hide the selections - dynamically create them
- No layout with tables. Everything done with CSS
- No use of innerHTML
- Include comments in the code to clearly explain functionality
- Re-direction of non-modern browsers. Redirect the user to Firefox download page

