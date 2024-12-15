using Microsoft.EntityFrameworkCore;
using ZigitTest.Data;

namespace ZigitTest.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly AppDbContext _context;

        public EmailRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetProviders()
        {
            List<string> providers = await _context.EmailProviders.Select(ep => ep.Name).ToListAsync();

            return providers;
        }
    }
}
