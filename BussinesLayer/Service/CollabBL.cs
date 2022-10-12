using BussinesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Service
{
    public class CollabBL : ICollabBL
    {
		private readonly ICollabRL collabRL;
		public CollabBL(ICollabRL collabRL)
		{
			this.collabRL = collabRL;
		}
        public CollabEntity CreateCollab(long UserId, long NoteId, Collaborator collaborator)
        {
			try
			{
				return this.collabRL.CreateCollab(UserId, NoteId, collaborator);
			}
			catch (Exception ex)
			{
				throw ex;
			}
        }
    }
}
