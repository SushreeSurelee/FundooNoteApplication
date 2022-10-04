using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        public IConfiguration configuration { get; }

        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration; 
        }
        public UserEntity UserRegistration(Registration registration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registration.FirstName;
                userEntity.LastName = registration.LastName;
                userEntity.EmailID = registration.EmailID;
                userEntity.Password = registration.Password;

                fundooContext.UserTable.Add(userEntity);
                int result=fundooContext.SaveChanges();
                if(result>0)
                {
                    return userEntity;
                }
                else 
                { 
                    return null; 
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(Login login)
        {
            try
            {
                var result = fundooContext.UserTable.Where(u => u.EmailID == login.EmailID && u.Password == login.Password).FirstOrDefault();
                if(result!=null)
                {
                    return GenerateSecurityToken(login.EmailID, result.UserId);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GenerateSecurityToken(string email,long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("KEY_TO_GENARATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
