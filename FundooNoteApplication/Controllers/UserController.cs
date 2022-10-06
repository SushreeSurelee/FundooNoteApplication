using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNoteApplication.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult Registration(Registration registration)
        {
            try 
            {
                var result = userBL.UserRegistration(registration);
                if(result!= null)
                {
                    return this.Ok(new { sucess = true, message = "User Registration Sucessfull.", data = result });
                }
                else 
                {
                    return this.BadRequest(new { sucess = false, message = "User Registration Unsucessfull." });
                }
            }
            catch (Exception ex)
            {
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
                    return this.Ok(new { sucess = true, message = "Login Sucessfull.", data = result });
                }
                else
                {
                    return this.NotFound(new { sucess = false, message = "Login Unsucessfull. Email or password is Invalid." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("Forget_Password")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgetPassword(email);
                if(result != null)
                {
                    return this.Ok(new { sucess = true, message = "Password reset mail has sent sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Failed to send the email. Please enter registred email ID." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
