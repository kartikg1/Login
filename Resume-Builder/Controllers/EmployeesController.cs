using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Resume_Builder.Models;

namespace Resume_Builder.Controllers
{
    public class EmployeesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }
            //var emp = from employee in db.Employees
            //          join projectTeam in db.ProjectTeams on employee.EmployeeId equals projectTeam.EmployeeId
            //          join project in db.Projects on projectTeam.ProjectId equals project.ProjectId
            //          where employee.EmployeeId == id
            //          select new
            //          {
            //              employee.EmployeeId,
            //              employee.EmployeeName,
            //              employee.EmployeeEmail,
            //              employee.EmployeePhone,
            //              employee.EmployeeDOB,
            //              employee.EmployeeDesignation,
            //              employee.EmployeeAddress,
            //              project.ProjectTitle,
            //              project.ProjectDescription,
            //              project.StartDate,
            //              project.EndDate
            //          };

            return Ok(employee);
        }

        [Route("api/EmployeeList")]
        [HttpGet]
        public IHttpActionResult GetEmployeeList()
        {
            var team = from employee in db.Employees
                       join projectTeam in db.ProjectTeams on employee.EmployeeId equals projectTeam.EmployeeId
                       join project in db.Projects on projectTeam.ProjectId equals project.ProjectId

                       select new
                       {
                           employee.EmployeeId,
                           employee.EmployeeName,
                           project.ProjectTitle,
                           projectTeam.Role,
                           projectTeam.EmployeeTech
                       };

            return Ok(team);
        }


        [ResponseType(typeof(Employee))]
        [Route("api/Employees/ProjectDetails/{id}")]
        public IHttpActionResult GetProjectDetails(int id)
        {
            var emp = from employee in db.Employees
                      join projectTeam in db.ProjectTeams on employee.EmployeeId equals projectTeam.EmployeeId
                      join project in db.Projects on projectTeam.ProjectId equals project.ProjectId
                      where employee.EmployeeId == id
                      select new
                      {
                          project.ProjectTitle,
                          project.ProjectDescription,
                          project.StartDate,
                          project.EndDate
                      };

            return Ok(emp);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeId == id) > 0;
        }
    }
}