using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(long UserId, long NoteId, string LabelName);
    }
}
