﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolProject.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFname { get; set; }
        public string StudentLname { get; set; }
        public string StudentNumber { get; set; }
        public string EnrolDate { get; set; }
    }
}