using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolProject.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ActionResult List()
        {
            List < Classes> Classes = new List<Classes>();
            ClassesDataController Controller = new ClassesDataController();
            Classes = Controller.ListClasses();

            return View(Classes);
        }

        public ActionResult Show(int id)
        {
            // Use the classes data controller find class method
            ClassesDataController Controller = new ClassesDataController();
            Classes SelectedClasses = Controller.FindClasses(id);

            // Navigate to Views/Classes/Show.cshtml
            return View(SelectedClasses);
        }
    }
}