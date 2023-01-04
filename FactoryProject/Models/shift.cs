using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class Shift
	{
        [Key]
        public int id { get; set; }
        public DateTime Date { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
    }
}

