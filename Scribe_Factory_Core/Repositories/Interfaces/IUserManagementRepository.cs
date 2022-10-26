using Scribe_Factory_Core.Common;
using Scribe_Factory_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Repositories.Interfaces
{
    public interface IUserManagementRepository
    {
        public UserLoginViewModel LoginUser(string username, string password);
        public bool RegisterUser(RegisterUserViewModel registerUserViewModel);

        public bool IsUserExist(string email);
    }
}
