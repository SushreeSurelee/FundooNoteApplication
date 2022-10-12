using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
        }

        [HttpPost("CreateCollaborator")]
        public IActionResult CreateCollab(long NoteId, Collaborator collaborator)
        {
            try
            {
                long UserId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.collabBL.CreateCollab(UserId, NoteId, collaborator);
                if(result!=null)
                {
                    return this.Ok(new { Success = true, message = "Collaborated Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to collaborate note" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
