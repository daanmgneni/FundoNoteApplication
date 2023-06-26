using CommonLayer.Models;
using DataLayer.DB;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface INotesDL
    {
        public NotesEntity AddNote(NoteModel notes);
        public bool CheckUserId(long userID);
        public NotesEntity DeleteNote(long NoteId);
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId);
        public bool Pinned(long NoteID);
        public bool Trashed(long NoteID);
        public bool Archieved(long NoteID);
        public NotesEntity ColorNote(long NoteId, string color);
        public string Imaged(long NoteID, IFormFile image);
        public IEnumerable<NotesEntity> Search(string query);
        public List<NotesEntity> GetAllNote();
    }
}
