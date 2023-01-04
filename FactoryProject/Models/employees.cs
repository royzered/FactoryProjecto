using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class Employees
    { 
            [Key]
        public int id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public int startYear { get; set; }
        public int departmentID { get; set; }

    }
}


