using MySql.Data.MySqlClient;
using SchoolProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolProject.Controllers
{
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/StudentData/ListStudents")]

        public List<Student> ListStudents()
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Sql Command - select * students
            string query = "select * from students";

            // Openning a connection
            Conn.Open();

            // Create a MySql Command
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            // Execute the command & store it in a result set 
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            // Create a list of student
            List<Student> Students = new List<Student>();

            // Loop through the result set
            while (ResultSet.Read())
            {
                // In the loop, retrieve the student info 
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFname = ResultSet["studentfname"].ToString();
                string StudentLname = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                string EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]).ToString();
                
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFname = StudentFname;
                NewStudent.StudentLname = StudentLname;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrolDate = EnrolDate;

                Students.Add(NewStudent);
            }

            // Close the connection
            Conn.Close();

            // Return the student name
            return Students;
        }


        // Return the student with an ID matching an integer
        [HttpGet]
        [Route("api/StudentData/FindStudent/{StudentId}")]

        public Student FindStudent(int StudentId)
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Openning a connection
            Conn.Open();

            // SQL Command
            string query = "select * from students where studentid =" + StudentId;

            // Creating SQL Command
            MySqlCommand Cmd = Conn.CreateCommand();
            Cmd.CommandText = query;

            // Executing the command & store it in a result set 
            MySqlDataReader ResultSet = Cmd.ExecuteReader();

            // Information into an student object
            Student NewStudent = new Student();

            // Loop
            while (ResultSet.Read())
            {

                NewStudent.StudentId = Convert.ToInt32(ResultSet["studentid"]);
                NewStudent.StudentFname = ResultSet["studentfname"].ToString();
                NewStudent.StudentLname = ResultSet["studentlname"].ToString();
                NewStudent.StudentNumber = ResultSet["studentnumber"].ToString();
                NewStudent.EnrolDate = Convert.ToDateTime(ResultSet["enroldate"]).ToString();
            }

            return NewStudent;
        }
    }
}
