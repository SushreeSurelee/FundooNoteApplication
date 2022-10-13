using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long userId, long noteId, string labelName);
        public List<LabelEntity> GetAllLabel(long userId);
    }
}
