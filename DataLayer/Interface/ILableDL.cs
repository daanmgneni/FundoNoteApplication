using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface ILableDL
    {
        public ResponseLable CreateLable(long notesId, long jwtUserId, LableModel model);
        public IEnumerable<LabelEntity> GetAllLable();
        public ResponseLable UpdateLable(long lableid, UpdateLabelModel model);
        public LabelEntity DeleteLable(long noteid);
    }
}
