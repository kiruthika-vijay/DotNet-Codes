using Microsoft.EntityFrameworkCore;

namespace BasicAuthenticationWebAPICode.Models
{
    public class UserRepository : IUserRepository
    {

        //private List<User> users = new List<User>
        //    {

        //        new User { Id = 1, Username = "admin", Password = "admin" },
        //        new User { Id = 2, Username = "user", Password = "user" },
        //        new User { Id = 3, Username = "Pranaya", Password = "Test@1234" },
        //        new User { Id = 4, Username = "Kumar", Password = "Admin@123" }
        //    };

        private readonly UserDBContext _context;

        public UserRepository(UserDBContext context)
        {
            _context = context;
        }

        public async Task<User?> ValidateUser(string username, string password)
        {
            await Task.Delay(100);
            //return users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            await Task.Delay(100);
            //return users.ToList();
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            await Task.Delay(100);
            //return users.FirstOrDefault(u => u.Id == id);
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddUser(User user)
        {
            await Task.Delay(100);
            //if (users.Any(u => u.Id == user.Id))
            //{
            //    throw new Exception("User already exists with the given ID.");
            //}
            //users.Add(user);
            //return user;
            if (_context.Users.Any(u => u.Id == user.Id))
            {
                throw new Exception("User already exists with the given ID.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            await Task.Delay(100);
            //var existingUser = await GetUserById(user.Id);
            //if (existingUser == null)
            //{
            //    throw new Exception("User not found.");
            //}
            //existingUser.Username = user.Username;
            //existingUser.Password = user.Password;
            //return existingUser;
            var existingUser = await _context.Users.FindAsync(user.Id);
            if(existingUser == null)
            {
                throw new Exception("User not found.");
            }
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task DeleteUser(int id)
        {
            //await Task.Delay(100);
            //var user = await GetUserById(id);
            //if (user == null)
            //{
            //    throw new Exception("User not found.");
            //}
            //users.Remove(user);
            var userr = await GetUserById(id);
            if(userr == null)
            {
                throw new Exception("User not found.");
            }
            _context.Users.Remove(userr);
            await _context.SaveChangesAsync();
        }
    }

}
