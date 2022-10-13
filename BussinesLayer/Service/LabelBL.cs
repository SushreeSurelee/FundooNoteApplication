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
        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName)
        {
			try
			{
				return this.labelRL.CreateLabel(UserId, NoteId, LabelName);
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
