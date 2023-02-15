using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoCivilizationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using Cora.Functionss;
using Cora.DataBase;
using Cora.Models;

namespace EcoCivilizationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Application>> GetAllApplication()
        {
            var applications = new List<Application>(ApplicationServices.GetAllAplications());
            var applicationsModel = new List<Application>(); 
            foreach(var aplication in applications)
            {
                applicationsModel.Add(new Application(aplication));
            }

            return applicationsModel ;
        }

        [HttpGet("{id}")]
        public ActionResult<Application> GetApplication(int id) 
        { 
            var application = ApplicationServices.GetApplication(id); 
            if (application == null) 
                return NotFound();
            return application;
        }

        [HttpGet("{date, idCity}")]
        public ActionResult<List<Application>> GetFilteredApplication(DateTime selectDate, int IDCity)
        {
            var application = ApplicationServices.GetFilterApplication(selectDate, IDCity);
            if (application == null)
                return NotFound();
            return application;
        }
    }
}
