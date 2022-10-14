using BussinesLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollabController> logger;
        public LabelController(ILabelBL labelBL, IDistributedCache distributedCache, ILogger<CollabController> logger)
        {
            this.labelBL = labelBL;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }
        [HttpPost("Create")]
        public IActionResult CreateLabel(long noteId, string labelName)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.labelBL.CreateLabel(userId, noteId, labelName);
                if(result!=null)
                {
                    logger.LogInformation("Label is created Successfully");
                    return this.Ok(new { success = true, message = "Label is created Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Unable to Label note");
                    return this.BadRequest(new { success = false, message = "Unable to Label note" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }      
        }
        [HttpGet("GetLabel")]
        public async Task<IActionResult> GetAllLabel()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var cachekey = "LabelList";
                string serializedLabelList;
                var labelResult = labelBL.GetAllLabel(userId);

                var redisLabelList = await distributedCache.GetAsync(cachekey);

                if (redisLabelList != null)
                {
                    serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                    labelResult = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
                }
                else
                {
                    serializedLabelList = JsonConvert.SerializeObject(labelResult);
                    redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                    await distributedCache.SetAsync(cachekey, redisLabelList, options);
                }
                logger.LogInformation("All Label Fetched Successfully");
                return this.Ok(new { success = true, message = "All Label Fetched Successfully", data = labelResult });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateLabel(long noteId, long labelId, string labelName)
        {
            try
            {
                var result = this.labelBL.UpdateLabel(noteId, labelId, labelName);
                if (result != null)
                {
                    logger.LogInformation("Label Updated Successfully");
                    return this.Ok(new { success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    logger.LogInformation("Unable to Update Label");
                    return this.BadRequest(new { success = false, message = "Unable to Update Label" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                var result = this.labelBL.DeleteLabel(labelId);
                if (result)
                {
                    logger.LogInformation("Label Deleted Successfully");
                    return this.Ok(new { success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    logger.LogInformation("Unable to Delete Label note");
                    return this.BadRequest(new { success = false, message = "Unable to Delete Label note" });
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
