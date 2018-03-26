﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using RESTControl.DAL_Simulation;
using RESTControl.Interfaces;

namespace RESTControl.RESTControllers
{
    /// <summary>
    /// REST Api User Controller
    /// </summary>
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserController _userController;

        public UserController(IUserController controller)
        {
            _userController = controller;
        }

        [HttpGet("{username}/{password}", Name = "GetUser")]
        public IActionResult Get(string username, string password)
        {

            var user = _userController.GetUser(username, password);

            if (user == null)
            {
                return NotFound("Wrong username or password");
            }

            return new ObjectResult(user);
        }

        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                var result = _userController.CreateUser(user);

                if(result != null)
                {
                    return CreatedAtRoute("GetUser", new { username = result.Username, password = result.Password }, result);
                }else
                {
                    return BadRequest("Username already exists");
                }             
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{username}/{password}")]
        public IActionResult Put(string username, string password, [FromBody] User user)
        {
            if (ModelState.IsValid)
            {

                var result = _userController.UpdateUser(username, password, user);

                if (result != null)
                {
                    return CreatedAtRoute("GetUser", new { username = result.Username, password = result.Password }, result);
                }else
                {
                    return BadRequest("Wrong username or password");
                }                
            }
            else
            {
                return BadRequest("Not a valid user");
            }
        }

    }
}