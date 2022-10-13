using BussinesLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;

namespace FundooNoteApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        public LabelController(ILabelBL labelBL)
        {
            this.labelBL = labelBL;
        }
        [HttpPost("CreateLabel")]
        public IActionResult CreateLabel(long noteId, string labelName)
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.labelBL.CreateLabel(userId, noteId, labelName);
                if(result!=null)
                {
                    return this.Ok(new { success = true, message = "Label is created Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Label note" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }      
        }
        [HttpGet("GetAllLabel")]
        public IActionResult GetAllLabel()
        {
            try
            {
                long userId = long.Parse(User.FindFirst("userId").Value.ToString());
                var result = this.labelBL.GetAllLabel(userId);
                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Label Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Fetch Label" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateLable")]
        public IActionResult UpdateLabel(long noteId, long labelId, string labelName)
        {
            try
            {
                var result = this.labelBL.UpdateLabel(noteId, labelId, labelName);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Unable to Update Label" });
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
