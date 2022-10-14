using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabBL collabBL, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.distributedCache = distributedCache;
        }

        [HttpPost("Create")]
        public IActionResult CreateCollab(long NoteId, Collaborator collaborator)
        {
            try
            {
                long UserId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.collabBL.CreateCollab(UserId, NoteId, collaborator);
                if(result!=null)
                {
                    return this.Ok(new { Success = true, message = "Collaborated Successfully", data = result });
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
        [HttpGet("GetCollab")]
        public async Task<IActionResult> GetAllCollab()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var cachekey = "CollabList";
                string serializedCollabList;
                var collabResult = collabBL.GetAllCollab(userId);

                var redisCollabList = await distributedCache.GetAsync(cachekey);

                if (redisCollabList != null)
                {
                    serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                    collabResult = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
                }
                else
                {
                    serializedCollabList = JsonConvert.SerializeObject(collabResult);
                    redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await distributedCache.SetAsync(cachekey, redisCollabList, options);
                }
                return this.Ok(new { success = true, message = "All Collab are fetched Successfully", data = collabResult });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteCollab(long collabId)
        {
            var result = this.collabBL.DeleteCollab(collabId);
            if (result)
            {
                return this.Ok(new { Success = true, message = "Deleted Successfully" });
            }
            else
            {
                return this.BadRequest(new { success = false, message = "Unable to delete" });
            }
        }
    }
}
