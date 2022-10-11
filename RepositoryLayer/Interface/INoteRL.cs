using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity UserNoteCreation(long userId, Note createNote);
        public List<NoteEntity> GetNote(long userId);
        public bool DeleteNote(long userId, long noteId);
        public NoteEntity UpdateNote(long userId, long noteId, Note note);
        public bool PinnedNote(long noteId);
        public bool ArchiveNote(long noteId);
        public bool Trashed(long noteId);
        public bool NoteColour(long noteId, string colour);
        public NoteEntity Image(long noteId, IFormFile img);
    }
}
