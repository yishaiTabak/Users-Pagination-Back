using Microsoft.EntityFrameworkCore;
using ZigitTest.Data;
using ZigitTest.Models;
using Bogus;

namespace ZigitTest.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> getUsers(int skip, int take)
        {
            List<UserModel> users = await _context.Users
                                                    .OrderBy(u => u.Id)
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToListAsync();

            return users;

        }

        public async Task<int> CountUsers()
        {
            int count = await _context.Users.CountAsync();
            return count;
        }
        public async Task InsertFakeUsers(int count)
        {
            var faker = new Faker<UserModel>()
          .RuleFor(u => u.Name, f => f.Name.FullName())  
          .RuleFor(u => u.Email, f => f.Internet.Email()) 
          .RuleFor(u => u.Age, f => f.Random.Int(18, 65));  

            List<UserModel> users = faker.Generate(count);

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}