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
                DateTime Hiredate = Convert.ToDateTime(ResultSet["hiredate"]);
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

            // Return the teachers names
            return Teachers;
        }

        /// <summary>
        /// Find an specific teacher from the list using the id
        /// </summary>
        /// <param name="TeacherId"></param>
        /// <returns>
        /// A teacher looked up from the search
        /// </returns>
        /// <example>
        /// localhost: api/TeacherData/FindTeacher/6
        /// </example>

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
                NewTeacher.HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                NewTeacher.Salary = Convert.ToInt32(ResultSet["salary"]);
            }

            return NewTeacher;
        }

        // Add teacher

        /// <summary>
        /// Receives teacher information and enters it into the database
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teacher's table</param>
        /// <returns></returns>
        /// <example>
        /// >curl -H "Content-Type: application/json" -d @teacher.json http://localhost:51326/api/teacherdata/addteacher
        /// POST: api/TeacherData/AddTeacher
        /// FORM DATA / REQUEST BODY / POST CONTENT
        /// {
        ///     "TeacherID": "23536",
        ///     "TeacherFname": "Julia",
        ///     "TeacherLname": "Park",
        ///     "EmployeeNumber": "2677563",
        ///     "HireDate": "Jun 35 2023",
        ///     "Salary": "835.03",
        /// }
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();

            // Openning a connection
            Conn.Open();

            //debugging
            Debug.WriteLine("API for adding an teacher");
            Debug.WriteLine(NewTeacher.TeacherId);

            // SQL Command
            MySqlCommand Cmd = Conn.CreateCommand();

            // How to receive a teacher
            string query = "insert into teachers (teacherid, teacherfname, teacherlname, employeenumber, hiredate, salary) values (@teacherid, @teacherfname, @teacherlname, @employeenumber, @hiredate, @salary)";

            Cmd.CommandText = query;
            Cmd.Parameters.AddWithValue("@teacherid", 0);
            Cmd.Parameters.AddWithValue("@teacherfname", NewTeacher.TeacherFname);
            Cmd.Parameters.AddWithValue("@teacherlname", NewTeacher.TeacherLname);
            Cmd.Parameters.AddWithValue("@employeenumber", NewTeacher.EmployeeNumber);
            Cmd.Parameters.AddWithValue("@hiredate", NewTeacher.HireDate);
            Cmd.Parameters.AddWithValue("@salary", NewTeacher.Salary);
            Cmd.ExecuteNonQuery(); // Executing the insert statement
            Conn.Close();
        }

        // Delete Teacher 
        /// <summary>
        /// Deletes a teacher in the system
        /// </summary>
        /// <param name="teacherId">The Teacher Id</param>
        /// <returns></returns>
        /// <example>
        /// POST: api/TeacherData/DeleteTeacher/5
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{TeacherId}")]
        public void DeleteTeacher(int teacherId)
        {
            // Access database
            MySqlConnection Conn = School.AccessDatabase();
            // Openning a connection
            Conn.Open();

            // SQL Command
            MySqlCommand Cmd = Conn.CreateCommand();
            string query = "delete from teachers where teacherid = @teacherid";
            Cmd.CommandText = query;

            Cmd.Parameters.AddWithValue("@teacherid", teacherId);
            Cmd.Prepare();
            Cmd.ExecuteNonQuery(); // Executing the insert statement
            Conn.Close();
        }

        // Update Teacher 
        /// <summary>
        /// Receive teacher data and a teacher id, and update the teacher in to the MySQL Database. 
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the author's table. </param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/210
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
        [Route("api/TeacherData/UpdateTeacher/{TeacherId}")]
        public void UpdateTeacher(int TeacherId, [FromBody]Teacher UpdatedTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(UpdatedTeacher.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            string query  = "update teachers set teacherfname=@teacherfname, teacherlname=@teacherlname,  employeenumber=@employeenumber, hiredate=@hiredate, salary=@salary where teacherid=@teacherid";

            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@teacherid", TeacherId);
            cmd.Parameters.AddWithValue("@teacherfname", UpdatedTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@teacherlname", UpdatedTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeenumber", UpdatedTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hiredate", UpdatedTeacher.HireDate);
            cmd.Parameters.AddWithValue("@salary", UpdatedTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
    }
}
