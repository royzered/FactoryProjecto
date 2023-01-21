using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace FactoryProject.Models
{
    public class Shift
	{
        [Key]
        public int id { get; set; }
        public DateOnly Date { get; set; } //used this instead of DateTime to show Date only, otherwise it'll show also time [00:00 in this case]
        public int startTime { get; set; }
        public int endTime { get; set; }

    }
}

