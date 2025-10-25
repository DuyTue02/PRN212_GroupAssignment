using DAL.Entities;
using System.Linq;

namespace DAL.Repositories
{
    public class UserAccountRepository
    {
        private readonly EVRentalDBContext _context;

        public UserAccountRepository()
        {
            _context = new EVRentalDBContext();
        }
        public User GetUserAccount(string email, string password)
        {            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}