using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.DataSimulation;
using StudentApi.Models;
using System.Security.Claims;

namespace StudentApi.Controllers
{
    [Authorize]
    [ApiController]
    /* [Route("[controller]")] // Sets the route for this controller to "students", based on the controller name.
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
        [Authorize(Roles = "Admin")]
        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            if (StudentDataSimulation.StudentsList.Count == 0)
                return NoContent();

            return Ok(StudentDataSimulation.StudentsList);
        }


        [AllowAnonymous]
        [HttpGet("Passed", Name = "GetPassedStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<Student>> GetPassedStudents()
        {
            var passedStudents = StudentDataSimulation.StudentsList
                .Where(student => student.Grade >= 50)
                .ToList();

            if (passedStudents.Count == 0)
                return NoContent();

            return Ok(passedStudents);
        }


        [AllowAnonymous]
        [HttpGet("AverageGrade", Name = "GetAverageGrade")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Student>> GetStudentByIdAsync(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            var authResult = await authorizationService.AuthorizeAsync(User, id, "StudentOwnerOrAdmin");
            if (!authResult.Succeeded)
                return Forbid();

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(student => student.Id == id);
            if (student == null)
                return NotFound("Student not found.");

            return Ok(student);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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


        [Authorize(Roles = "Admin")]
        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Student> UpdateStudent(int id, Student updatedStudent)
        {
            if (id < 1)
                return BadRequest($"Not accepted ID {id}");

            if (updatedStudent == null)
                return BadRequest("Request body is empty.");

            if (string.IsNullOrEmpty(updatedStudent.Name) || updatedStudent.Age < 0 || updatedStudent.Grade < 0)
                return BadRequest("Invalid student data.");

            var student = StudentDataSimulation.StudentsList.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound($"Student with ID {id} not found.");

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Grade = updatedStudent.Grade;

            return Ok(student);
        }
    }
}