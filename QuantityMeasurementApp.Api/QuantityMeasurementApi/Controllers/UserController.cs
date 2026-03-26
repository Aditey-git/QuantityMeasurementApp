using Microsoft.AspNetCore.Mvc;
using QuantityAppService;
using QuantityAppModel;
using QuantityAppRepository;
using Microsoft.VisualBasic;
using System.Runtime.Versioning;


namespace QuantityMeasurementApi.Contollers
{
    [ApiController]
    [Route("api/auth")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationServices authService;

        public UserController(IAuthenticationServices auth)
        {
            authService = auth;
        }

    
        [HttpPost("register")]
        public IActionResult Register(string email, string password)
        {
            authService.RegisterUser(email, password);

            return Ok("User Registered Successfully.");
        } 

    
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            string token = authService.LoginUser(email, password);

            if (token == null)
            {
                return Unauthorized("Invalid Email or Password!");
            }

            return Ok(new {token});
        }
    }
}
