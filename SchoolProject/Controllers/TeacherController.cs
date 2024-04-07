using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using SchoolProject.Controllers;
using SchoolProject.Models;
using System.ComponentModel;


namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        /// <summary>
        /// Show a teacher list
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns>
        /// returns a page listing teachers in the system
        /// </returns>

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

            // Navigate to Views/Teacher/List.cshtml
            return View(Teachers);
        }

        // GET: Teacher information
        //  localhost: xx/Teacher/Show/{id} -> show a particular teacher matching that ID
        public ActionResult Show(int id)
        {
            // Use the teacher data controller find teacher method
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            // Navigate to Views/Teacher/Show.cshtml
            return View(SelectedTeacher);
        }

        // GET : localhost: xx/Teacher/New -> Show a new teacher webpage
        public ActionResult New()
        {
            // Navigate to Views/Teacher/New.cshtml
            return View();
        }

        // POST: localhost:xx/Teacher/Create 
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary) 
        {
            // Debugging message
            // Confirm we received the new teacher information
            Debug.WriteLine("Teacher Create Method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);
            
            // Add the teacher in
            TeacherDataController TeacherController = new TeacherDataController();

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;

            TeacherController.AddTeacher(NewTeacher);

            // Redirects to List.cshtml
            return RedirectToAction("List");
        }

        //GET: /Teacher/ConfirmDelete/{id} -> a webpage which lets the user confirm their choice to delete
        [HttpGet]
        public ActionResult ConfirmDelete(int id) {  
            TeacherDataController TeacherController = new TeacherDataController();

            Teacher SelectedTeacher = TeacherController.FindTeacher(id);

            return View(SelectedTeacher);
        }

        // POST: /Teacher/Delete/{id} -> the teacher list page
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController TeacherController = new TeacherDataController();

            TeacherController.DeleteTeacher(id); 

            return RedirectToAction("List");
        }
    }
}