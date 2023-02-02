using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class EmpShift
	{
        [Key]
        public int id { get; set; }
        public int employeeID { get; set; }
        public string shiftDate { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public string? EmployeeName { get; set; }
    }
}

