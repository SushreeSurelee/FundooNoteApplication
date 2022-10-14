using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entities;
using System;
using System.Security.Claims;

namespace FundooNoteApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;
        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }
        [HttpPost("Register")]
        public IActionResult Registration(Registration registration)
        {
            try 
            {
                var result = userBL.UserRegistration(registration);
                if(result!= null)
                {
                    logger.LogInformation("User Registration Succesfull");
                    return this.Ok(new { sucess = true, message = "User Registration Sucessfull.", data = result });
                }
                else 
                {
                    logger.LogInformation("User Registration Unsuccesfull");
                    return this.BadRequest(new { sucess = false, message = "User Registration Unsucessfull." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPost("Login")]
        public IActionResult UserLogin(Login login)
        {
            try
            {
                var result = this.userBL.UserLogin(login);
                if (result != null)
                {
                    logger.LogInformation("Login Sucessfull.");
                    return this.Ok(new { sucess = true, message = "Login Sucessfull.", data = result });
                }
                else
                {
                    logger.LogInformation("Login Unsucessfull. Email or password is Invalid.");
                    return this.NotFound(new { sucess = false, message = "Login Unsucessfull. Email or password is Invalid." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgetPassword(email);
                if(result != null)
                {
                    logger.LogInformation("Password reset mail has sent sucessfully");
                    return this.Ok(new { sucess = true, message = "Password reset mail has sent sucessfully" });
                }
                else
                {
                    logger.LogInformation("Failed to send the email. Please enter registred email ID.");
                    return this.BadRequest(new { sucess = false, message = "Failed to send the email. Please enter registred email ID." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult RsetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email, password, confirmPassword);
                if (result)
                {
                    logger.LogInformation("Password reset is successfull");
                    return this.Ok(new { success = true, message = "Password reset is successfull" });
                }  
                else
                {
                    logger.LogInformation("Password reset is failed. Please enter valid password");
                    return this.BadRequest(new { success = false, message = "Password reset is failed. Please enter valid password" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
    }
}
