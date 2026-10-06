using Microsoft.AspNetCore.Mvc;


namespace _13___FirstRESTProject.Controllers
{
    /*
        [Route("api/[controller]")]

            Dynamic route: [controller] becomes the class name minus "Controller".
        Rename the class → the URL changes → any external client hard-coded to
        the old URL breaks.

            Use a hard-coded string ("api/MyFirstAPI") when the endpoint is a public
        contract with external consumers.
        Use [controller] when only your own frontend consumes the API (it lives
        in the same repo and gets updated alongside).

            For this project: hard-coded, because we want the URL to stay stable
        even if the class is renamed.
    */

    [Route("api/TestProject")]
    [ApiController]
    public class FirstProjectController : ControllerBase
    {
        [HttpGet("MyName", Name = "MyName")]
        public string GetMyName()
        {
            return "My Name is Unis";
        }

        [HttpGet("YourName", Name = "YourName")]
        public string GetYourName()
        {
            return "Your Name is JY";
        }

        [HttpGet("sum/{num1}/{num2}")]
        public int Sum2Numbers(int Num1, int Num2)
        {
            return Num1 + Num2;
        }

        [HttpGet("multi/{num1}/{num2}")]
        public int Multi2Numbers(int Num1, int Num2)
        {
            return Num1 * Num2;
        }
    }
}
