using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using DataLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL : ICollabBL
    {
		public readonly ICollaboratorDL collaboratorDL;
        public CollabBL(ICollaboratorDL collaboratorDL)
        {
              this.collaboratorDL = collaboratorDL;
        }
        public CollaboratorEntity AddCollaborate(long notesId,long uesrid, CollabModel model)
        {
			try
			{
                return collaboratorDL.AddCollaborate(notesId, uesrid, model);
            }
			catch (Exception)
			{

				throw;
			}
        }
        public CollaboratorEntity DeleteCollaborator(long collaboratorID)
        {
            try
            {
                return collaboratorDL.DeleteCollaborator(collaboratorID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<CollaboratorEntity> GetCollab()
        {
            try
            {
                return collaboratorDL.GetCollab();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
