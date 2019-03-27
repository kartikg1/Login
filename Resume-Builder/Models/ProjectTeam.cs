using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Resume_Builder.Models
{
    public class ProjectTeam
    {
        [Key]
        public int TeamId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public string Role { get; set; }
        public string EmployeeTech { get; set; }
        [DataType(DataType.Date)]
        public DateTime EmployeeStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EmployeeEndDate { get; set; }
        [NotMapped]
        public string EmployeeEmail { get; set; }
        public Boolean ProjectStatus { get; set; }

        //Navigation Property
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}