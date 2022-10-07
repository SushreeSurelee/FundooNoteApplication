using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System.Linq;
using System;
using System.Security.Claims;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }
        [HttpPost("CreateNote")]
        public IActionResult UserNoteCreation(Note createNote)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                //var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.noteBL.UserNoteCreation(userId, createNote);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Note Creation is Sucessfull.", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Note Creation is Unsucessfull." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
