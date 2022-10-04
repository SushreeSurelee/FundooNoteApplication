using BussinesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public UserEntity UserRegistration(Registration registration)
        {
            try 
            {
                return userRL.UserRegistration(registration);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(string email, string password)
        {
            try
            {
                return this.userRL.UserLogin(email, password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
