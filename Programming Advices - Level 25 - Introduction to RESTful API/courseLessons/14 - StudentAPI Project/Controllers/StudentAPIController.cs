using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.DataSimulation;


namespace StudentApi.Controllers
{
    [ApiController]
    /* 
        [Route("[controller]")] // Sets the route for this controller to "students", based on the controller name.
    */
    [Route("api/Students")]

    public class StudentsController : ControllerBase 
    {
        /// <summary>
        /// GET /api/Students — returns the full list of students.
        ///
        /// Return type is <c>IEnumerable&lt;Student&gt;</c> instead of <c>List&lt;Student&gt;</c>:
        /// - Caller only needs to iterate, not mutate.
        /// - We can swap the underlying storage later without changing this signature.
        /// - <c>ActionResult</c> wraps the response so we can return different HTTP
        ///   status codes (200 Ok, 404, 500) depending on the outcome.
        /// </summary>
        [HttpGet] // Marks this method to respond to HTTP GET requests.
        public ActionResult<IEnumerable<Student>> GetAllStudents() // Define a method to get all students.
        {
            return Ok(StudentDataSimulation.StudentsList); // Returns the list of students.
        }

        [HttpGet("GetStudentByID/{ID}")]
        public ActionResult<Student> GetStudentByID(int ID)
        {
            var _Student = StudentDataSimulation.StudentsList.FirstOrDefault(x => x.Id == ID);
            if (_Student == null)
                return NotFound();

            return Ok(_Student);
        }
    }
}