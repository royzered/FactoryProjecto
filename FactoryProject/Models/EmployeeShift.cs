using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class EmployeeShift
	{
        [Key]
        public int id { get; set; }
        public int employeeID { get; set; }
        public string? employeeName { get; set; }
        public int shiftID { get; set; }
    
    }
}

