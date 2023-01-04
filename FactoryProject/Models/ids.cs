using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class ids
	{
        [Key]
        public int id { get; set; }
        public int employeeID { get; set; }
        public int shiftID { get; set; }
    }
}

