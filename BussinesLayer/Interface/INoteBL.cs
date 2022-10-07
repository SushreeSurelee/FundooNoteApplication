using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity UserNoteCreation(string email, Note createNote);
    }
}
