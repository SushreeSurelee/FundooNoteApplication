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
        public IConfiguration Configuration { get; }

        public UserRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = configuration;
        }
        public UserEntity UserRegistration(Registration registration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = registration.FirstName;
                userEntity.LastName = registration.LastName;
                userEntity.EmailID = registration.EmailID;
                userEntity.Password = EncryptionPassword(registration.Password);

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
        public string EncryptionPassword(string password)
        {
            try
            {
                byte[] encode = ASCIIEncoding.ASCII.GetBytes(password);
                string encrypted = Convert.ToBase64String(encode);
                return encrypted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DecryptionPassword(string encodedData)
        {
            try
            {
                byte[] decode = Convert.FromBase64String(encodedData);
                string decrypted = ASCIIEncoding.ASCII.GetString(decode);
                return decrypted;
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
                var result = this.fundooContext.UserTable.FirstOrDefault(e => e.EmailID == login.EmailID);
                string decodePass = DecryptionPassword(result.Password);
                if(decodePass ==login.Password && result != null)
                {
                    return GenerateSecurityToken(result.EmailID,result.UserId);
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
        public string ForgetPassword(string email)
        {
            try
            {
                var emailCheck= fundooContext.UserTable.FirstOrDefault(e => e.EmailID == email);
                if(emailCheck!=null)
                {
                    var token= GenerateSecurityToken(emailCheck.EmailID,emailCheck.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();
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
        public bool ResetPassword(string email,string password, string confirmPassword)
        {
            try
            {
                if(password.Equals(confirmPassword))
                {
                    var result = fundooContext.UserTable.Where(e => e.EmailID == email).FirstOrDefault();
                    result.Password = password;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
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
            var key = Encoding.ASCII.GetBytes(Configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
