using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoCivilizationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using Cora.Functionss;
using Cora.DataBase;

namespace EcoCivilizationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("{login, password}")]
        public ActionResult<User> GetAuthorizationUser(string login, string password)
        {
            var userExcist = UserServices.AuthorizationUser(login, password);
            if (userExcist == null)
                return NotFound();
            return userExcist;
        }
    }
}
