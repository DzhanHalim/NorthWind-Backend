using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet("getbyemail")]

        public IActionResult GetByEmail(string email)
        {
            var result = _userService.GetByMail(email);

            if (result!=null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getclaims")]

        public IActionResult GetClaims(User user)
        {
            var result = _userService.GetClaims(user);

            if (result!=null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
