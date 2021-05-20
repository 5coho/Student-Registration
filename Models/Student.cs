/*
 * model for the post data
 * A student with 5 values: firstName, lastName, weekTime, classTime and startDate
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace StudentRegistration.Models
{

    public class Student
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string weekTime { get; set; }
        public string classTime { get; set; }
        public string startDate { get; set; }
    }

}