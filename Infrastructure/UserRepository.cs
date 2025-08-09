using Application.Interfaces;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppCoreContext _context;

        public UserRepository(AppCoreContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
             _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var userExist = await _context.Users.FindAsync(id);
            if (userExist != null)
            {
                 _context.Users.Remove(userExist);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();


        public async Task<User> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(s => s.Email == email);


        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task UpdateAsync(User user)
        {
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}
