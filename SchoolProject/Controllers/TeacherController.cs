using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using SchoolProject.Controllers;
using SchoolProject.Models;


namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        // GET: : localhost: xx/Teacher/List -> returns a page listing teachers in the system
        [HttpGet]

        public ActionResult List(string SearchKey)
        {
            //Debug 
            Debug.WriteLine("Received a Search Key of" + SearchKey);

            List<Teacher> Teachers = new List<Teacher>();
            TeacherDataController Controller = new TeacherDataController();
            Teachers = Controller.ListTeachers(SearchKey);

            // Navigate to Views/Article/List.cshtml
            return View(Teachers);
        }

        // GET: Teacher information
        //  localhost: xx/Teacher/Show/{id} -> show a particular article matching that ID
        public ActionResult Show(int id)
        {
            // Use the teacher data controller find teacher method
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            // Navigate to Views/Article/Show.cshtml
            return View(SelectedTeacher);
        }
    }
}