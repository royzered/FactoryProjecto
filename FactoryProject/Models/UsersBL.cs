﻿using System;
using FactoryProject.Data;

namespace FactoryProject.Models
{
	public class UsersBL
	{
        private readonly DataContext _context;

        public UsersBL(DataContext context)
        {
            _context = context;
        }

        private bool _auth = false;
        private  int _userId;

        public IEnumerable<Users> GetUsers()
		{
			return _context.Users.ToList();
		}
        public bool LogInUser(string? UserName, string? Password) 
        {
            var CheckUser = _context.Users.Where(user => user.userName == UserName && user.password == Password).First();
            _userId = CheckUser.id;
            if(CheckUser == null) 
            {
                return _auth = false;
            }
            else if(CheckUser != null) 
            {
                UserRequests();
                return _auth = true;
            }
            else
            {
                return _auth = false;
            }
        }

        public bool LogoutUser() {
            return _auth = false;
        }

        public int UserRequests () {
            var LoggedUser = _context.Users.Where(user => user.id == _userId).First();
            LoggedUser.numOfActions--;
            _context.SaveChanges();

            return LoggedUser.numOfActions;
        }

	}
}

