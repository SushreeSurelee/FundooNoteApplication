using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity UserNoteCreation(long userId, Note createNote);
        public List<NoteEntity> GetNote(long userId);
    }
}
