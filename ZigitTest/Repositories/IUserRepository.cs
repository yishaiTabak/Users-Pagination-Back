using ZigitTest.Models;

namespace ZigitTest.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserModel>> getUsers(int skip,int take);
        Task<int> CountUsers();
        Task InsertFakeUsers(int count);
        Task<FilteredUsersResponse> GetFilteredUsers(UserFilterRequest filters);
    }
}
