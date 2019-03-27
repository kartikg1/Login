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
    public class ProjectTeamsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProjectTeams
        public IQueryable<ProjectTeam> GetProjectTeams()
        {
            return db.ProjectTeams;
        }

        // GET: api/ProjectTeams/5
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult GetProjectTeam(int id)
        {
            // project = db.Projects.Find(id);
            
            
            
                var team = from employee in db.Employees
                           join projectTeam in db.ProjectTeams on employee.EmployeeId equals projectTeam.EmployeeId
                           join project in db.Projects on projectTeam.ProjectId equals project.ProjectId
                           where projectTeam.ProjectId == id
                           select new
                           {
                               project.ProjectDescription,
                               employee.EmployeeName,
                               employee.EmployeeEmail,
                               projectTeam.Role,
                               projectTeam.EmployeeTech
                           };
            //foreach (var e in team)
            //{
            //    return db.ProjectTeams;
            //}




            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/ProjectTeams/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProjectTeam(int id, ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != projectTeam.TeamId)
            {
                return BadRequest();
            }

            db.Entry(projectTeam).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectTeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProjectTeams
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult PostProjectTeam(ProjectTeam projectTeam)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // check if EmployeeEmailId exists
            var employeeEmails = db.Employees.Select(x => x.EmployeeEmail);
            if (!employeeEmails.Contains(projectTeam.EmployeeEmail))
            {
                return BadRequest("Invalid Email");
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            projectTeam.EmployeeId = db.Employees.SingleOrDefault(x => x.EmployeeEmail == projectTeam.EmployeeEmail).EmployeeId;

            // check if the Employee is already added to the project(redundant/multiple entries)
            var ActiveEmployeeIds = db.ProjectTeams.AsEnumerable()
                 .Where(x => x.ProjectId == projectTeam.ProjectId).Select(x => x.EmployeeId)
                 .ToList();
            if (ActiveEmployeeIds.Contains(projectTeam.EmployeeId))
            {
                return BadRequest("Employee Already exists in the project");
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            db.ProjectTeams.Add(projectTeam);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = projectTeam.TeamId }, projectTeam);
        }

        // DELETE: api/ProjectTeams/5
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult DeleteProjectTeam(int id)
        {
            ProjectTeam projectTeam = db.ProjectTeams.Find(id);
            if (projectTeam == null)
            {
                return NotFound();
            }

            db.ProjectTeams.Remove(projectTeam);
            db.SaveChanges();

            return Ok(projectTeam);
        }

        [Route("api/EmployeeTechCount")]
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult GetEmployeeTechCount()
        {


            var EmployeeTechCount = db.ProjectTeams.GroupBy(x => x.EmployeeTech)
                         .Select(group => new
                         {
                             Tech = group.Key,
                             Count = group.Count()
                         }
                         );

            return Ok(EmployeeTechCount);
        }

        [Route("api/EmployeeRoleCount")]
        [ResponseType(typeof(ProjectTeam))]
        public IHttpActionResult GetEmployeeRoleCount()
        {


            var EmployeeRoleCount = db.ProjectTeams.GroupBy(x => x.Role)
                         .Select(group => new
                         {
                             Tech = group.Key,
                             Count = group.Count()
                         }
                         );

            return Ok(EmployeeRoleCount);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectTeamExists(int id)
        {
            return db.ProjectTeams.Count(e => e.TeamId == id) > 0;
        }
    }
}