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
    public class StudentController : Controller
    {
        // GET: Students
        public ActionResult List()
        {
            List<Student> Students = new List<Student>();
            StudentDataController Controller = new StudentDataController();
            Students = Controller.ListStudents();

            return View(Students);
        }

        public ActionResult Show(int id)
        {
            // Use the student data controller find student method
            StudentDataController Controller = new StudentDataController();
            Student SelectedStudent = Controller.FindStudent(id);

            // Navigate to Views/Student/Show.cshtml
            return View(SelectedStudent);
        }
    }
}