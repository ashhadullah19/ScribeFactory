using Scribe_Factory_Core.Common;
using Scribe_Factory_Core.Models;
using Scribe_Factory_Core.Repositories.Interfaces;
using Scribe_Factory_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Scribe_Factory_Core.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        ScribeFactoryContext scribeFactoryContext = null;
        public UserManagementRepository(ScribeFactoryContext scribeFactoryContext)
        {
            this.scribeFactoryContext = scribeFactoryContext;
        }
        public UserLoginViewModel LoginUser(string username, string password)
        {
            var user = scribeFactoryContext.ApplicatioUsers.Where(x => x.Email == username && x.PasswordHash == MD5Hash(password)).FirstOrDefault();
            if (user != null)
            {
                UserLoginViewModel userLoginViewModel = new UserLoginViewModel() { User = user };
                return userLoginViewModel;
            }
            return null;
        }

        public bool RegisterUser(RegisterUserViewModel registerUserViewModel)
        {
            try
            {
                ApplicatioUser applicatioUser = new ApplicatioUser()
                {
                    City = registerUserViewModel.City,
                    Country = registerUserViewModel.Country,
                    Email = registerUserViewModel.Email,
                    FirstName = registerUserViewModel.FirstName,
                    LastName = registerUserViewModel.LastName,
                    PasswordHash = MD5Hash(registerUserViewModel.ConfirmPassword),
                    State = registerUserViewModel.State,
                    Zip = registerUserViewModel.Zip,
                };
                scribeFactoryContext.ApplicatioUsers.Add(applicatioUser);
                scribeFactoryContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool IsUserExist(string email)
        {
            var user = scribeFactoryContext.ApplicatioUsers.Where(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }

        private string MD5Hash(string password)
        {
            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();
            return encoded;
        }
    }
}
