namespace ZigitTest.Repositories
{
    public interface IEmailRepository
    {
        public Task<List<string>> GetProviders();
    }
}
