using BussinesLayer.Interface;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Service
{
    public class LabelBL : ILabelBL
    {
		private readonly ILabelRL labelRL;

		public LabelBL(ILabelRL labelRL)
		{
			this.labelRL = labelRL;
		}
        public LabelEntity CreateLabel(long userId, long noteId, string labelName)
        {
			try
			{
				return this.labelRL.CreateLabel(userId, noteId, labelName);
			}
			catch (Exception)
			{

				throw;
			}
        }
        public List<LabelEntity> GetAllLabel(long userId)
		{
			try
			{
				return this.labelRL.GetAllLabel(userId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        public LabelEntity UpdateLabel(long noteId, long labelId, string labelName)
		{
			try
			{
				return this.labelRL.UpdateLabel(noteId, labelId, labelName);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
        public bool DeleteLabel(long labelId)
		{
			try
			{
				return this.labelRL.DeleteLabel(labelId);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}
