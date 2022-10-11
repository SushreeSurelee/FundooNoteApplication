using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL:INoteRL
    {
        private readonly FundooContext fundooContext;
        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public NoteEntity UserNoteCreation(long userId,Note createNote)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                var result = fundooContext.UserTable.Where(user =>user.UserId == userId).FirstOrDefault();
                noteEntity.Title = createNote.Title;
                noteEntity.Description = createNote.Description;
                noteEntity.Reminder = createNote.Reminder;
                noteEntity.Colour = createNote.Colour;
                noteEntity.Image = createNote.Image;
                noteEntity.Archive = createNote.Archive;
                noteEntity.Pinned = createNote.Pinned;
                noteEntity.Trash = createNote.Trash;
                noteEntity.Created = createNote.Created;
                noteEntity.Edited = createNote.Edited;
                noteEntity.UserID = result.UserId;

                fundooContext.NoteTable.Add(noteEntity);
                int update = fundooContext.SaveChanges();
                if(update>0)
                {
                    return noteEntity;
                }
                else
                {
                    return null;
                }
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
                var result = fundooContext.NoteTable.Where(note => note.UserID == userId).ToList();
                return result;
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
                var result = fundooContext.NoteTable.Where(note => note.UserID == userId && note.NoteId == noteId).FirstOrDefault();
                if (result != null)
                {
                    result.Title = note.Title;
                    result.Description = note.Description;
                    result.Reminder = note.Reminder;
                    result.Image = note.Image;
                    result.Created = note.Created;
                    result.Edited = note.Edited;

                    fundooContext.NoteTable.Update(result);
                    var update= fundooContext.SaveChanges();
                    if(update>0)
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                    
                }
                else
                    return null;
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
                var result = fundooContext.NoteTable.Where(note => note.UserID == userId && note.NoteId == noteId).FirstOrDefault();
                if (result!=null)
                {
                    fundooContext.NoteTable.Remove(result);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.NoteTable.Where(note => note.NoteId == noteId).FirstOrDefault();
                if(result.Pinned==false)
                {
                    result.Pinned = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Pinned = false;
                    fundooContext.SaveChanges();
                    return false;
                }
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
                var result = fundooContext.NoteTable.Where(note => note.NoteId == noteId).FirstOrDefault();
                if (result.Archive == false)
                {
                    result.Archive = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Archive = false;
                    fundooContext.SaveChanges();
                    return false;
                }
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
                var result = fundooContext.NoteTable.Where(note => note.NoteId == noteId).FirstOrDefault();
                if (result.Trash == false)
                {
                    result.Trash = true;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    result.Trash = false;
                    fundooContext.SaveChanges();
                    return false;
                }
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
                var result = fundooContext.NoteTable.Where(note => note.NoteId == noteId).FirstOrDefault();
                if(result.Colour!=colour)
                {
                    result.Colour = colour;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NoteEntity Image(long noteId, IFormFile file)
        {
            try
            {
                var result = this.fundooContext.NoteTable.Where(note => note.NoteId == noteId).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                        "dnleksaub",
                        "428761563518582",
                        "txR9BhSHSmKn6e9ChNKjXVnO2SM");

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };
                    var upload = cloudinary.Upload(uploadParams);
                    string filePath = upload.Url.ToString();
                    result.Image = filePath;
                    fundooContext.SaveChanges();
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
