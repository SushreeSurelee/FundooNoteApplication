using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity UserRegistration(Registration registration);
        public string UserLogin(Login login);
        public string GenerateSecurityToken(string email, long userId);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
    }
}
