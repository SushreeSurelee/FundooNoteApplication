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

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL noteBL, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.distributedCache = distributedCache;
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
        public async Task<IActionResult> GetNote()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var cachekey = Convert.ToString(userId);
                string serializeddata;
                List<NoteEntity> result;

                var distcacheresult = await distributedCache.GetAsync(cachekey);

                if (distcacheresult != null)
                {
                    serializeddata = Encoding.UTF8.GetString(distcacheresult);
                    result = JsonConvert.DeserializeObject<List<NoteEntity>>(serializeddata);

                    return this.Ok(new { success = true, message = "Note Data fetch Successfully", data = result });
                }
                else
                {
                    var noteResult = this.noteBL.GetNote(userId);
                    serializeddata = JsonConvert.SerializeObject(noteResult);
                    distcacheresult = Encoding.UTF8.GetBytes(serializeddata);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    await distributedCache.SetAsync(cachekey, distcacheresult, options);
                    if (noteResult != null)
                    {
                        return this.Ok(new { sucess = true, message = "All notes are fetched and now you can read your notes", data = noteResult });
                    }
                    else
                    {
                        return this.BadRequest(new { sucess = false, message = "Unable to show the notes." });
                    }
                }
            }
            catch (Exception ex)
            {
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
        [HttpDelete("Delete")]
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
        [HttpPut("Pin")]
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
        [HttpPut("Archive")]
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
        [HttpPut("Trash")]
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
        [HttpPut("Colour")]
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
