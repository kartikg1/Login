using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Resume_Builder.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePhone { get; set; }
        public DateTime EmployeeDOB { get; set; }
        public string EmployeeDesignation { get; set; }
        public string EmployeeAddress { get; set; }

    }
}