using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactoryProject.Models
{
	public class Departments
    { 
            [Key]
        public int id { get; set; }

        public string? departmentName { get; set; }
        public int manager { get; set; }
    }
}


