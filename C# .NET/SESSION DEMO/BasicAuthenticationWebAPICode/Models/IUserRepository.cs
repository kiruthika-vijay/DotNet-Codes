namespace BasicAuthenticationWebAPICode.Models
{
    public interface IUserRepository
    {
        Task<User?> ValidateUser(string username, string password);
        //Task<List<User>> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(int id);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
