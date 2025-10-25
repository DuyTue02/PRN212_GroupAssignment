using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class UserAccountService
    {
        private readonly UserAccountRepository _repository;

        public UserAccountService()
        {
            _repository = new UserAccountRepository();
        }

        public User GetUserAccount(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            return _repository.GetUserAccount(email, password);
        }
    }
}
