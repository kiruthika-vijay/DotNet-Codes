using Microsoft.EntityFrameworkCore;

namespace RoleBasedAuthenticationCodeDemo.Models
{
    public class UserRepository : IUserRepository
    {

        //private List<User> users = new List<User>
        //    {

        //        new User { Id = 1, Username = "admin", Password = "admin", Roles = new List<Role> { new Role { RoleId = 1, RoleName = "Admin" } } },
        //        new User { Id = 2, Username = "user", Password = "user", Roles = new List<Role> { new Role { RoleId = 1, RoleName = "User" } } },
        //        new User { Id = 3, Username = "Pranaya", Password = "Test@1234", Roles = new List<Role> { new Role { RoleId = 1, RoleName = "Admin" } } },
        //        new User { Id = 4, Username = "Kumar", Password = "Admin@123", Roles = new List<Role> { new Role { RoleId = 1, RoleName = "Admin" } }  }
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
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            await Task.Delay(100);
            //return users.ToList();
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            await Task.Delay(100);
            //return users.FirstOrDefault(u => u.Id == id);
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> AddUser(User user, List<int> roleIds)
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
            // Add User to Users table
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            // Ensure roles exist in the Roles table and add associations in UserRole table
            foreach (var roleId in roleIds)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    throw new Exception($"Role with ID {roleId} does not exist.");
                }

                // Add entry to UserRole junction table
                _context.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                });
            }
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user, List<int> roleIds)
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
            var existingUser = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            // Update user details
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;

            // Remove existing roles
            _context.UserRoles.RemoveRange(existingUser.UserRoles);

            // Add new roles
            foreach (var roleId in roleIds)
            {
                var role = await _context.Roles.FindAsync(roleId);
                if (role == null)
                {
                    throw new Exception($"Role with ID {roleId} does not exist.");
                }

                _context.UserRoles.Add(new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                });
            }

            await _context.SaveChangesAsync();

            return existingUser;
        }


        public async Task DeleteUser(int id)
        {
            await Task.Delay(100);
            //var user = await GetUserById(id);
            //if (user == null)
            //{
            //    throw new Exception("User not found.");
            //}
            //users.Remove(user);
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Remove related entries in UserRole
            _context.UserRoles.RemoveRange(user.UserRoles);

            // Remove user
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
