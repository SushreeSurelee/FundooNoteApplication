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
        public NoteEntity UserNoteCreation(string email, Note createNote)
        {
            try
            {
                return this.noteRL.UserNoteCreation(email, createNote);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
