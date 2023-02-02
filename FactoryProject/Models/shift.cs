using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
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

