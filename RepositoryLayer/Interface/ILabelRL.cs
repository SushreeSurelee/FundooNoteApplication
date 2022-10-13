using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(long userId, long noteId, string labelName);
        public List<LabelEntity> GetAllLabel(long userId);
    }
}
