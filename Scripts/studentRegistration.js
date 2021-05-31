/*
    This File handles a couple page initializations like setting the date in the form and
    and hide the Completed Registration section.

    All functions relating to submission of the form are also in this file
*/


//scripts only run after the website is loaded
$(document).ready(function(){

    //setting the current date into the input date field
    setDate();

    //hiding the complete section
    $(".complete").hide()

    //handles when the register button is clicked
    $("form").submit(function(event){

        //stops the submit button from refreshing page.
        event.preventDefault();
        
        //calling the submitRegistartion
        //all the work is done by this function
        submitRegistration();

    });


});


//Takes all of the form data, creates an object, then sends to server
//also checks if the data is non-empty
function submitRegistration(){

    //creating vars from the form data
    //All white space from first and last names are removed
    var firstName = $("#firstName").val().replace(/\s/g, "");
    var lastName = $("#lastName").val().replace(/\s/g, "");
    var weekTime = $('input[name="weekTime"]:checked').val();
    var classTime = $('input[name="classTime"]:checked').val();
    var startTime = $("#startDate").val();

    //getting the data from the form and creating an object
    var data = createObject(firstName, lastName, weekTime, classTime, startTime);

    //printing data to be sent
    console.log(data);

    // checking to make data is null or ""
    // if not exit function and alert
    // doesn't take into consideration people with one name like Cher
    if(dataCheck(data)){
        return alert("Make sure all form fields are completed");
    }

    //communicate with server at loopback port 8080
    //if something goes wrong, alert
    $.post("Home/RegisterStudent", data)

        //this function runs when the post was successful
        .done(function (resultData) {

            //printing resultData to console
            console.log(resultData);

            //hides the registration section
            $(".registration").hide();

            //adding data to client side
            $("#userName").append(resultData.userName);
            $("#time").append(resultData.classTime);
            $("#date").append(resultData.startDate);

            //showing the complete section
            $(".complete").show();

    })
        //the post failed, alert called
        .fail(function(){
            alert("Opps! Something went wrong!");
        });
}


//sets the current date to form field
//also sets minumum date as today
function setDate(){

    //getting todays date
    var today = new Date();

    //formatting year month and day for display
    var date = String(today.getDate()).padStart(2, "0");
    var month = String(today.getMonth()+1).padStart(2, "0");
    var year = String(today.getFullYear())

    //setting the string to diplay
    var displayDate = year + "-" + month + "-" + date;

    //setting value of form date element and min
    var element = document.getElementById("startDate");
    element.value = displayDate;
    element.min = displayDate;
    
}


//returns an object give the parameters
//used for creating data to be sent to the server
//I've used this instead of serializing because this form is fixed and will never change
//and I think this is easier to understand and read
function createObject(firstName, lastName, weekTime, classTime, startDate){

    var data = 
    {
        firstName: firstName,
        lastName: lastName,
        weekTime: weekTime,
        classTime: classTime,
        startDate: startDate
    };

    return data;
}


//checks to ensure that data in and object is not "" or null
function dataCheck(data){

    //iterates through data
    for(key in data){
        if (data[key] == "" || data[key] == null){
            return true;
        }
    }

}
