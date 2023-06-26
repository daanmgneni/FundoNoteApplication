using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollaboratorEntity AddCollaborate(long notesId,long uesrid, CollabModel model);
        public CollaboratorEntity DeleteCollaborator(long collaboratorID);
        public IEnumerable<CollaboratorEntity> GetCollab();
    }
}
