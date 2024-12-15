using Microsoft.EntityFrameworkCore;
using ZigitTest.Data;
using ZigitTest.Models;
using Bogus;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


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
            var emailProviders = await _context.EmailProviders.ToListAsync();

            var faker = new Faker<UserModel>()
              .RuleFor(u => u.Name, f => f.Name.FullName())  
              .RuleFor(u => u.Age, f => f.Random.Int(18, 65))
              .RuleFor(u => u.EmailProvider, f => f.PickRandom(emailProviders))
              .RuleFor(u => u.Email, (f, u) => $"{f.Internet.UserName()}@{u.EmailProvider.Name}.com");

            List<UserModel> users = faker.Generate(count);

            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
        public async Task<FilteredUsersResponse> GetFilteredUsers(UserFilterRequest filters)
        {
            IQueryable<UserModel> query = _context.Users.AsQueryable();

            query = this.ApplyNameFilter(query,filters.Search.Name);
            query = this.ApplyEmailFilter(query, filters.Search.Email);

            int totalCount = await query.CountAsync();

            query = query.OrderBy($"{filters.Sorting.Field} {filters.Sorting.Order}");

            int skip = filters.Pagination.PageSize * (filters.Pagination.Page - 1);
            query = query.Skip(skip).Take(filters.Pagination.PageSize);

            List<UserModel> users = await query.ToListAsync();

            return new FilteredUsersResponse
            {
                Users = users,
                TotalCount = totalCount
            };
        }
        private IQueryable<UserModel> ApplyNameFilter(IQueryable<UserModel> query,string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                string[] searchWords = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                query = query = query.Where(u => searchWords.All(word =>
                EF.Functions.Like(u.Name,word +"%") ||
                EF.Functions.Like(u.Name, "% " + word + "%")));
            }
            return query;
        }
        private IQueryable<UserModel> ApplyEmailFilter(IQueryable<UserModel> query, string[] emailProviders)
        {
            if (emailProviders != null && emailProviders.Length != 0)
            {
                query = query.Where(u => emailProviders.Contains(u.EmailProvider.Name));
            }
            return query;
        }
    }
}