using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(long UserId, long NoteId, Collaborator collaborator);
        public List<CollabEntity> GetAllCollab(long UserId);
    }
}
