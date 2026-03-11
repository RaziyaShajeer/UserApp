using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services.UserModule.DTO;
using Domain.Services.UserModule.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.UserModule
{
	public class UserRepository:IUserRepository
	{
       
        public UserContext _context;
		public UserRepository(UserContext context)
		{
			_context = context;
		}
        public async Task<bool> UserRegistration(Users user)
        {
           
                var userExist = await _context.Users
                                .AnyAsync(e => e.EmailId == user.EmailId);

                if (userExist)
                {
                    throw new UserAlreadyExistException("User with this email already exists");
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            
        }
        public async Task<Users> UserLoginAsync(string Email, string Password)
        {
            var user = await _context.Users.Where(e=>e.EmailId== Email && e.Password==Password).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }

            return null ;
        }
        public async Task<List<Users>> GetUsersAsync()
        {
            var users = await _context.Users
              .OrderByDescending(x => x.CreatedAt)
              .ToListAsync();

            return (users);
        }
        public async Task<int> GetUserCountAsync()
        {
           return await _context.Users.CountAsync();
            
        }
        public async Task<List<Users>> lastFiveUsersAsync()
        {
          return  await _context.Users
                               .OrderByDescending(u => u.CreatedAt)
                               .Take(5)
                               .ToListAsync();
            
        }
    }
}
