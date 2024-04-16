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

        // Updating teachers
        
        // GET: /Teacher/Update/{teacherid} --> A webpage asking the user to update a teacher
        public ActionResult Update(int id) {

            // Use the teacher data controller find teacher method
            TeacherDataController TeacherController = new TeacherDataController();
            Teacher SelectedTeacher = TeacherController.FindTeacher(id);

            return View(SelectedTeacher);
        }


        // POST: /Teacher/Edit/{teacherid} --> Sending the teacher information to update 

        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated bio of the teacher.</param>
        /// <param name="HireDate">The updated email of the teacher.</param>
        /// <param name="Salary">The updated email of the teacher.</param>

        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Julia",
        ///	"TeacherLname":"Wilcott",
        ///	"EmployeeNumber":"54234",
        ///	"HireDate":"12/4/2024",
        ///	"Salary":"12,932"
        /// } 
        /// </example>
        [HttpPost]
        public ActionResult Edit(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            TeacherDataController TeacherController = new TeacherDataController();

            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.TeacherFname = TeacherFname;
            UpdatedTeacher.TeacherLname = TeacherLname;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.HireDate = HireDate;
            UpdatedTeacher.Salary = Salary;

           TeacherController.UpdateTeacher(id, UpdatedTeacher);

            return RedirectToAction("Show/" + id);
        }
    }
}