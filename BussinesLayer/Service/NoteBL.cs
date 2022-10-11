using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Service
{
    public class NoteBL : INoteBL
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
        public NoteEntity UpdateNote(long userId, long noteId, Note note)
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
        public NoteEntity PinnedNote(long noteId)
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
        public NoteEntity ArchiveNote(long noteId)
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
        public NoteEntity Trashed(long noteId)
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
        public NoteEntity NoteColour(long noteId, string colour)
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
        public NoteEntity Image(long noteId, IFormFile img)
        {
            try
            {
                return this.noteRL.Image(noteId, img);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
