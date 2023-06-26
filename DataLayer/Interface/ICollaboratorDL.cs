using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface ICollaboratorDL
    {
        public CollaboratorEntity AddCollaborate(long notesId, long userid, CollabModel model);
        public CollaboratorEntity DeleteCollaborator(long collaboratorID);
        public IEnumerable<CollaboratorEntity> GetCollab();
    }
}
