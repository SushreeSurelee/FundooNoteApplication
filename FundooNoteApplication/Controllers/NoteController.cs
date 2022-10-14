using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System.Linq;
using System;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Channels;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NoteController> logger;
        public NoteController(INoteBL noteBL, IDistributedCache distributedCache, ILogger<NoteController> logger)
        {
            this.noteBL = noteBL;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }
        [HttpPost("Create")]
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
                    logger.LogInformation("Note Creation is Sucessfull.");
                    return this.Ok(new { sucess = true, message = "Note Creation is Sucessfull.", data = result });
                }
                else
                {
                    logger.LogInformation("Note Creation is Unsucessfull.");
                    return this.BadRequest(new { sucess = false, message = "Note Creation is Unsucessfull." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpGet("GetNote")]
        public async Task<IActionResult> GetNote()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var cachekey = "NoteList";
                string serializedNoteList;
                var noteResult = this.noteBL.GetNote(userId);

                var redisNoteList = await distributedCache.GetAsync(cachekey);

                if (redisNoteList != null)
                {
                    serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
                    noteResult = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNoteList);
                }
                else
                {
                    serializedNoteList = JsonConvert.SerializeObject(noteResult);
                    redisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await distributedCache.SetAsync(cachekey, redisNoteList, options);
                }
                logger.LogInformation("All notes are fetched Successfully");
                return this.Ok(new { success = true, message = "All notes are fetched Successfully", data = noteResult });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
 
        [HttpPut("Update")]
        public IActionResult UpdateNote(long noteId, Note note)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = noteBL.UpdateNote(userId, noteId, note);
                if (result!=null)
                {
                    logger.LogInformation("Note updated Successfully");
                    return this.Ok(new { success = true, message = "Note updated Successfully",data=result });
                } 
                else
                {
                    logger.LogInformation("Unable to update note");
                    return this.BadRequest(new { success = false, message = "Unable to update note" });
                } 
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNote(long noteId)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.noteBL.DeleteNote(userId, noteId);
                if (result)
                {
                    logger.LogInformation("Note deleted successfully.");
                    return this.Ok(new { success = true, message = "Note deleted successfully." });
                }
                else
                {
                    logger.LogInformation("Unable to delete the note.");
                    return this.BadRequest(new { success = false, message = "Unable to delete the note." });
                }
                    
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("Pin")]
        public IActionResult PinnedNote(long noteId)
        {
            try
            {
                var result = this.noteBL.PinnedNote(noteId);
                if (result.Pinned==true)
                { 
                    logger.LogInformation("Note is pinned successfully.");
                    return this.Ok(new { success = true, message = "Note is pinned successfully.", data = result });
                }
                else if (result.Pinned==false)
                {
                    logger.LogInformation("Note is unpinned sucessfully");
                    return this.Ok(new { success = true, message = "Note is unpinned sucessfully", data = result });
                }
                else
                {
                    logger.LogInformation("Unable to pin the note.");
                    return this.BadRequest(new { success = false, message = "Unable to pin the note." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("Archive")]
        public IActionResult ArchiveNote(long noteId)
        {
            try
            {
                var result = this.noteBL.ArchiveNote(noteId);
                if (result.Archive == true)
                {
                    logger.LogInformation("Note is Archived successfully.");
                    return this.Ok(new { success = true, message = "Note is Archived successfully.", data = result });
                }
                else if (result.Archive == false)
                {
                    logger.LogInformation("Note is unarchived sucessfully");
                    return this.Ok(new { success = true, message = "Note is unarchived sucessfully", data = result });
                }
                else
                {
                    logger.LogInformation("Unable to archive the note.");
                    return this.BadRequest(new { success = false, message = "Unable to archive the note." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("Trash")]
        public IActionResult Trashed(long noteId)
        {
            try
            {
                var result = this.noteBL.Trashed(noteId);
                if (result.Trash==true)
                {
                    logger.LogInformation("Note is moved to Trash successfully.");
                    return this.Ok(new { success = true, message = "Note is moved to Trash successfully.",data=result });
                }
                else if (result.Trash==false)
                {
                    logger.LogInformation("Note is untrashed sucessfully.");
                    return this.Ok(new { success = true, message = "Note is untrashed sucessfully.",data = result });
                }
                else
                {
                    logger.LogInformation("Unable to trash the note.");
                    return this.BadRequest(new { success = false, message = "Unable to trash the note." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("Colour")]
        public IActionResult NoteColour(long noteId, string colour)
        {
            try
            {
                var result = this.noteBL.NoteColour(noteId,colour);
                if (result!=null)
                {
                    logger.LogInformation("Note colour changed successfully.");
                    return this.Ok(new { success = true, message = "Note colour changed successfully.",data=result });
                }
                else
                {
                    logger.LogInformation("Entered colour is same as current note colour.");
                    return this.BadRequest(new { success = false, message = "Entered colour is same as current note colour." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Image uploaded sucessfully");
                    return this.Ok(new { success = true, message = "Image uploaded sucessfully", Response = result });
                }
                else
                {
                    logger.LogInformation("Failed to upload Image");
                    return this.BadRequest(new { success = false, message = "Failed to upload Image" });
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
