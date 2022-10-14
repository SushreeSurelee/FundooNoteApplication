using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public LabelEntity CreateLabel(long userId,long noteId,string labelName)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity();
                labelEntity.UserId = userId;
                labelEntity.NoteId = noteId;
                labelEntity.LabelName = labelName;
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
        public List<LabelEntity> GetAllLabel(long userId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(u => u.UserId == userId).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LabelEntity UpdateLabel(long noteId,long labelId,string labelName)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(u => u.NoteId == noteId && u.LabelId == labelId).FirstOrDefault();
                if(result!=null)
                {
                    result.LabelName = labelName;
                    fundooContext.LabelTable.Update(result);
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
        public bool DeleteLabel(long labelId)
        {
            try
            {
                var result = fundooContext.LabelTable.Where(u => u.LabelId == labelId).FirstOrDefault();
                if(result!=null)
                {
                    fundooContext.LabelTable.Remove(result);
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
    }
}
