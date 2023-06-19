using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity AddNote(NoteModel notes);
        public bool CheckUserId(long userID);
        public NotesEntity DeleteNote(long NoteId);
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId);
        public bool Pinned(long NoteID);
        public bool Trashed(long NoteID);
        public bool Archieved(long NoteID);
        public NotesEntity ColorNote(long NoteId, string color);
    }
}
