using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using SchoolProject.Models;


namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// List the teachers in the system. Filters by teacher matching a search key.
        /// </summary>
        /// <returns>
        /// <param  name="SearchKey">The search key for our teachers</param>
        /// Returns a list of teachers in the system
        /// </returns>
        /// 

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey}")]

        public List<Teacher> ListTeachers(string SearchKey)
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Debug
            Debug.WriteLine("Data access search key " + SearchKey);

            // Openning a connection
            Conn.Open();

            // Sql Command - select * teachers
            string query = "select * from teachers where teacherFname like'%" + SearchKey + "%' or hiredate like '%" + SearchKey + "%' or salary like '%" + SearchKey + "'";

            // Create a MySql Command
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            // Execute the command & store it in a result set 
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            // Create a list of teacher 
            List<Teacher> Teachers = new List<Teacher>();

            // Loop through the result set
            while (ResultSet.Read())
            {
                // In the loop, retrieve the teacher info 
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                string Hiredate = Convert.ToDateTime(ResultSet["hiredate"]).ToString();
                decimal TeacherSalary = Convert.ToInt32(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = Hiredate;
                NewTeacher.Salary = TeacherSalary;

                Teachers.Add(NewTeacher);
            }

            // Close the connection
            Conn.Close();

            // Return the article titles
            return Teachers;
        }

        // Return the teacher with an ID matching an integer
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]

        public Teacher FindTeacher(int TeacherId)
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Openning a connection
            Conn.Open();

            // SQL Command
            string query = "select * from teachers where teacherid =" + TeacherId;

            // Creating SQL Command
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            // Executing the command & store it in a result set 
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            // Information into an teacher object
            Teacher NewTeacher = new Teacher();

            // Loop
            while (ResultSet.Read())
            {

                NewTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                NewTeacher.TeacherFname = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLname = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]).ToString();
                NewTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);
            }

            return NewTeacher;
        }
    }
}
