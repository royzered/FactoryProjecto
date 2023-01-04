using System;
using System.ComponentModel.DataAnnotations;

namespace FactoryProject.Models
{
	public class Users
	{
		[Key]
		public int id { get; set; }
		public string? fullName { get; set; }
		public string? userName { get; set; }
		public string? password { get; set; }
		public int numOfActions { get; set; }

    }
}

