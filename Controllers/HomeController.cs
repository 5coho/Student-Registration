/*
 * controller to handle the jquery post
 * takes care of logic for generating the user's username, time of the first class
 * and figuring out the start date given the preferred start date
 * 
 */


using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Student_Registration.Controllers
{


    public class HomeController : Controller
    {


        [HttpPost]
        //handles the post call by Jquery and gets the data from the html form
        //returns a json object that contains username, start date and start time
        public ActionResult RegisterStudent(Student data)
        {

            //calling the getUserName function to create the user's username
            string name = getUserName(data.firstName, data.lastName);

            //calling the getStartTime and getting the... time of the first class
            string time = getStartTime(data.classTime);

            //calling the getStartDate to get the start date given preferred start
            string start = getStartDate(data.startDate, data.weekTime);

            //returning the json
            return Json(new { userName = name , classTime = time , startDate = start });

        }


        //creates and returns the userName as defined in the exercise
        //<lastname><first initial><random 4 digitnumber>
        //After generating a new user name there is now check to ensure duplication or conflict
        //I have though about this but for the sake of the exercise did not include a check
        private string getUserName(string firstName, string lastName)
        {

            //creating the random object
            Random rand = new Random();

            //generating the 4 digit number
            int number = rand.Next(1000, 9999);

            //getting the first name initial
            string initial = firstName.Substring(0, 1);

            //concatenating strings
            string temp = lastName + initial + number;

            //making temp all lowercase
            string userName = temp.ToLower();

            return userName;

        }


        //just gets the starting time of the first class as defined by the document
        //Morning -> 8:00am, Afternoon -> 1:30pm, Evening -> 7:00pm
        private string getStartTime(string classTime)
        {

            //checks the string value of classTime and returns the appropriate time as a string
            if (classTime == "mornings")
            {
                return "8:00am";
            }
            else if (classTime == "afternoons")
            {
                return "1:30pm";
            }
            else
            {
                return "7:00pm";
            }

        }


        //logic behind getting the start date as defined by the document
        //parameters are weekend/weekday and the preferred date
        //start date is at least 10 days after the preferred date
        private string getStartDate(string startDate, string weekTime)
        {
            //getting the preferred start date string and splitting it
            string[] dateElements = startDate.Split('-');

            //creating date object from the startDate string
            DateTime date = new DateTime(int.Parse(dateElements[0]), int.Parse(dateElements[1]), int.Parse(dateElements[2]));

            //adding 10 days to date
            DateTime datePlusTen = date.AddDays(10);

            //logic to find the proper day of the week
            DateTime start = getDayOfWeek(weekTime, datePlusTen);

            return start.ToString("dddd, dd MMMM yyyy");
        }
   

        //brute forcing to get the proper day of the week for the start date
        //keeps 
        private DateTime getDayOfWeek(string weekTime, DateTime date)
        {

            //keep adding days until reaching a weekend day or weekday day
            for (int i = 1; i <= 7; i++)
            {

                date = date.AddDays(1);

                if (weekTime == "weekend" && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    return date;
                }

                if (weekTime == "weekday" && (date.DayOfWeek == DayOfWeek.Monday || date.DayOfWeek == DayOfWeek.Tuesday || date.DayOfWeek == DayOfWeek.Wednesday || date.DayOfWeek == DayOfWeek.Thursday || date.DayOfWeek == DayOfWeek.Friday))
                {
                    return date;
                }

            }

            return date;

        }
        

    }

}