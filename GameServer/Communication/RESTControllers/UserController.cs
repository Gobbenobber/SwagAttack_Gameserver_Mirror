using System;
using Application.Interfaces;
using Communication.Filters;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Communication.RESTControllers
{
    // <summary>
    // Http Api User Controller
    // </summary>
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserController _userController;

        public UserController(IUserController controller)
        {
            _userController = controller;
        }

        [HttpPost("Login", Name = "GetUser")]
        [ValidateModelState]
        public IActionResult GetUser([FromBody] LoginDto loginInfo)
        {
            var result = _userController.GetUser(loginInfo.Username, loginInfo.Password);

            // User is found and has been logged in
            if (result != null)
                return new ObjectResult(result);

            return new NotFoundResult();
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult CreateUser([FromBody] User rawUser)
        {
            var result = _userController.CreateUser(rawUser);

            if (result != null) return CreatedAtRoute("GetUser", result);

            return BadRequest("Username already exists");
        }

        [HttpPut]
        [ValidateModelState]
        [AuthorizationSwag]
        public IActionResult UpdateUser([FromHeader] string username, [FromBody] User user)
        {
            if (username != null)
            {
                var result = _userController.UpdateUser(username, user);

                if (result != null) return CreatedAtRoute("GetUser", result);
            }

            return BadRequest();
        }

        public class LoginDto
        {
            private string _password;
            private string _username;

            public string Username
            {
                get => _username;
                set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Username must be set");

                    _username = value;
                }
            }

            public string Password
            {
                get => _password;
                set
                {
                    if (string.IsNullOrEmpty(value))
                        throw new ArgumentException("Password must be set");

                    _password = value;
                }
            }
        }
    }
}