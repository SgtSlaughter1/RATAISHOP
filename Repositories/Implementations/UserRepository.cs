﻿using Microsoft.EntityFrameworkCore;
using RATAISHOP.Context;
using RATAISHOP.Entities;
using RATAISHOP.Repositories.Interfaces;

namespace RATAISHOP.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly RataiDbContext _context;

        public UserRepository(RataiDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> GetUserByUsernameOrEmailAsync(string identifier)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == identifier || u.Email == identifier);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }

}
