using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public ResponseLable CreateLable(long notesId, long jwtUserId, LableModel model);
        public IEnumerable<LabelEntity> GetAllLable();
        public ResponseLable UpdateLable(long lableid, UpdateLabelModel model);
        public LabelEntity DeleteLable(long labelID);

    }
}
