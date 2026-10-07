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
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if (StudentDataSimulation.StudentsList.Count == 0)
                return NoContent();

            return Ok(StudentDataSimulation.StudentsList);
        }


        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudentDataSimulation.StudentsList
                .Where(student => student.Grade >= 50)
                .ToList();

            if (passedStudents.Count == 0)
                return NoContent();

            return Ok(passedStudents);
        }


        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<double> GetAverageGrade()
        {
            if (StudentDataSimulation.StudentsList.Count == 0)
                return NoContent();

            var averageGrade = StudentDataSimulation.StudentsList.Average(student => student.Grade);
            return Ok(averageGrade);
        }


        [HttpGet("{id}", Name = "GetStudentById")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(student => student.Id == id);
            if (student == null)
                return NotFound($"Student with ID {id} not found.");
            return Ok(student);
        }


        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public ActionResult<Student> AddStudent(Student newStudent)
        {
            if (newStudent == null || string.IsNullOrEmpty(newStudent.Name) || newStudent.Age < 0 || newStudent.Grade < 0)
                return BadRequest("Invalid student data.");

            newStudent.Id = StudentDataSimulation.StudentsList.Count > 0 ? StudentDataSimulation.StudentsList.Max(s => s.Id) + 1 : 1;
            StudentDataSimulation.StudentsList.Add(newStudent);

            return CreatedAtRoute(
                "GetStudentById",
                new { id = newStudent.Id },
                newStudent
            );
        }


        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            StudentDataSimulation.StudentsList.Remove(student);
            return NoContent();
        }
    }
}