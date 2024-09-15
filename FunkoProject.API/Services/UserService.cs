using FunkoProject.Data;
using FunkoProject.Data.Entities;
using FunkoProject.Exceptions;

namespace FunkoProject.Services;

    public interface IUserService
    {
        User GetUserById(string id);
    }

    public class UserService: IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public User GetUserById(string id)
        {
            var idAsInt = Int32.Parse(id);
            var user = _context.Users.FirstOrDefault(u => u.Id == idAsInt);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            return user;
        }
    }