using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity CreateLabel(long UserId,long NoteId,string LabelName)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.UserId = UserId;
                labelEntity.NoteId = NoteId;
                labelEntity.LabelName = LabelName;
                fundooContext.LabelTable.Add(labelEntity);
                int result = fundooContext.SaveChanges();
                if(result>0)
                {
                    return labelEntity;
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
