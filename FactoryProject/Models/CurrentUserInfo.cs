using System;
namespace FactoryProject.Models
{
	public class CurrentUserInfo
    { 
            public string token { get; set; }
            public int CurrentUserID { get; set; }
            public string CurrentUserName { get; set; }
             public string CurrentUserFullName { get; set; }

            public int CurrentuserActionsleft { get; set; }

    };
}

