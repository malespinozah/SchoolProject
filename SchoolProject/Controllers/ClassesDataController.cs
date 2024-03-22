using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class ClassesDataController : ApiController
    {

        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/ClassesData/ListClasses")]

        public List<Classes> ListClasses()
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Sql Command - select * classes
            string query = "select * from classes";

            // Openning a connection
            Conn.Open();

            // Create a MySql Command
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            // Execute the command & store it in a result set 
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            // Create a list of classes
            List<Classes> Classes = new List<Classes>();

            // Loop through the result set
            while (ResultSet.Read())
            {
                // In the loop, retrieve the classes info 
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string StartDate = Convert.ToDateTime(ResultSet["startdate"]).ToString();
                string FinishDate = Convert.ToDateTime(ResultSet["finishdate"]).ToString();
                string ClassName = ResultSet["classname"].ToString();

                Classes NewClasses = new Classes();
                NewClasses.ClassId = ClassId;
                NewClasses.ClassCode = ClassCode;
                NewClasses.TeacherId = TeacherId;
                NewClasses.StartDate = StartDate;
                NewClasses.FinishDate = FinishDate;
                NewClasses.ClassName = ClassName;

                Classes.Add(NewClasses);
            }

            // Close the connection
            Conn.Close();

            // Return the article titles
            return Classes;
        }


        // Return the student with an ID matching an integer
        [HttpGet]
        [Route("api/ClassestData/FindClasses/{ClassId}")]

        public Classes FindClasses(int ClassId)
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Openning a connection
            Conn.Open();

            // SQL Command
            string query = "select * from classes where classid =" + ClassId;

            // Creating SQL Command
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            // Executing the command & store it in a result set 
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            // Information into an classes object
            Classes NewClasses = new Classes();

            // Loop
            while (ResultSet.Read())
            {
                NewClasses.ClassId = Convert.ToInt32(ResultSet["classid"]);
                NewClasses.ClassCode = ResultSet["classcode"].ToString();
                NewClasses.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewClasses.StartDate = Convert.ToDateTime(ResultSet["startdate"]).ToString();
                NewClasses.FinishDate = Convert.ToDateTime(ResultSet["finishdate"]).ToString();
                NewClasses.ClassName = ResultSet["classname"].ToString();
            }

            return NewClasses;
        }
    }
}
