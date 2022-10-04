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
        public string UserLogin(string email, string password);
    }
}
