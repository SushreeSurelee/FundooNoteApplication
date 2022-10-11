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
        [HttpGet("GetNote")]
        public IActionResult GetNote()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.noteBL.GetNote(userId);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "All notes are fetched and now you can read your notes", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Unable to show the notes." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(long noteId, Note note)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = noteBL.UpdateNote(userId, noteId, note);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Note updated Successfully",data=result });
                } 
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to update note" });
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteNote")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.noteBL.DeleteNote(userId, noteId);
                if (result)
                {
                    return this.Ok(new { success = true, message = "Note deleted successfully." });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to delete the note." });
                }
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPut("PinNote")]
        public IActionResult PinnedNote(long noteId)
        {
            try
            {
                var result = this.noteBL.PinnedNote(noteId);
                if (result.Pinned==true)
                {
                    return this.Ok(new { success = true, message = "Note is pinned successfully.", data = result });
                }
                else if (result.Pinned==false)
                {
                    return this.Ok(new { success = true, message = "Note is unpinned sucessfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to pin the note." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("ArchiveNote")]
        public IActionResult ArchiveNote(long noteId)
        {
            try
            {
                var result = this.noteBL.ArchiveNote(noteId);
                if (result.Archive == true)
                {
                    return this.Ok(new { success = true, message = "Note is Archived successfully.", data = result });
                }
                else if (result.Archive == false)
                {
                    return this.Ok(new { success = true, message = "Note is unarchived sucessfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to archive the note." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("TrashNote")]
        public IActionResult Trashed(long noteId)
        {
            try
            {
                var result = this.noteBL.Trashed(noteId);
                if (result.Trash==true)
                {
                    return this.Ok(new { success = true, message = "Note is moved to Trash successfully.",data=result });
                }
                else if (result.Trash==false)
                {
                    return this.Ok(new { success = true, message = "Note is untrashed sucessfully.",data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to trash the note." });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPut("NoteColour")]
        public IActionResult NoteColour(long noteId, string colour)
        {
            try
            {
                var result = this.noteBL.NoteColour(noteId,colour);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Note colour changed successfully.",data=result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Entered colour is same as current note colour." });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UploadImage")]
        public IActionResult Image(long noteId, IFormFile img)
        {
            try
            {
                var result = this.noteBL.Image(noteId, img);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Image uploaded sucessfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to upload Image" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
