using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class EmpShift
	{
        [Key]
        public int id { get; set; }
        public int employeeID { get; set; }
        public int shiftID { get; set; }
        public DateTime shiftDate { get; set; }
        public string? EmployeeName { get; set; }
    }
}

