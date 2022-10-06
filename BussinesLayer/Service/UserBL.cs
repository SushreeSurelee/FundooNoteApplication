using BussinesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
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
        public string UserLogin(Login login)
        {
            try
            {
                return this.userRL.UserLogin(login);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GenerateSecurityToken(string email, long userId)
        {
            try
            {
                return this.userRL.GenerateSecurityToken(email,userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string ForgetPassword(string email)
        {
            try
            {
                return this.userRL.ForgetPassword(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return userRL.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
