namespace RoleBasedAuthenticationCodeDemo.Models
{
    public interface IUserRepository
    {
        Task<User?> ValidateUser(string username, string password);
        //Task<List<User>> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User> AddUser(User user, List<int> roleIds);
        Task<User> UpdateUser(User user, List<int> roleIds);
        Task DeleteUser(int id);
    }
}
