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
        public bool DeleteNote(long userId, long noteId);
        public bool UpdateNote(long userId, long noteId, Note note);
        public bool PinnedNote(long noteId);
        public bool ArchiveNote(long noteId);
        public bool Trashed(long noteId);
    }
}
