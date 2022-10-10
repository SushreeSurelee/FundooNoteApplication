using BussinesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Service
{
    public class NoteBL :INoteBL
    {
        private readonly INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public NoteEntity UserNoteCreation(long userId, Note createNote)
        {
            try
            {
                return this.noteRL.UserNoteCreation(userId, createNote);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<NoteEntity> GetNote(long userId)
        {
            try
            {
                return this.noteRL.GetNote(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateNote(long userId, long noteId, Note note)
        {
            try
            {
                return this.noteRL.UpdateNote(userId, noteId, note);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteNote(long userId, long noteId)
        {
            try
            {
                return this.noteRL.DeleteNote(userId, noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool PinnedNote(long noteId)
        {
            try
            {
                return this.noteRL.PinnedNote(noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool ArchiveNote(long noteId)
        {
            try
            {
                return this.noteRL.ArchiveNote(noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool Trashed(long noteId)
        {
            try
            {
                return this.noteRL.Trashed(noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public bool NoteColour(long noteId, string colour)
        {
            try
            {
                return this.noteRL.NoteColour(noteId, colour);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
