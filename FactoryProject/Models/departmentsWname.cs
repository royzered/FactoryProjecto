using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FactoryProject.Models
{
	public class DepartmentsWname
    { 
            [Key]
        public int id { get; set; }
        public string? departmentName { get; set; }
        public int? manager { get; set; }
        
        public string? managerName { get; set; }
        public bool? isEmpty { get; set; }
    }
}


