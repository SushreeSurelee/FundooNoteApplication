using Microsoft.Extensions.Configuration;
using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        private readonly FundooContext fundooContext;
        public CollabRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollabEntity CreateCollab(long UserId,long NoteId, Collaborator collaborator)
        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.UserId = UserId;
                collabEntity.NoteId = NoteId;
                collabEntity.CollabEmail = collaborator.CollabEmail;
                collabEntity.Edited = collaborator.Edited;
                fundooContext.CollabTable.Add(collabEntity);
                int result = fundooContext.SaveChanges();
                if(result>0)
                {
                    return collabEntity;
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
