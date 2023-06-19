using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Service
{
    public class NotesDL : INotesDL
    {
        private readonly FundoContext context;
        private readonly IConfiguration Config;
        public NotesDL(FundoContext context, IConfiguration Config)
        {
            this.context = context;
            this.Config = Config;

        }
        public bool CheckUserId(long userID)
        {
            try
            {

                var check = context.UserTable.FirstOrDefault(x => x.UserId == userID);
                if (check != null)
                {
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity AddNote(NoteModel notes)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = notes.Title;
                notesEntity.Note = notes.Note;
                notesEntity.Color = notes.Color;
                notesEntity.Image = notes.Image;
                notesEntity.IsArchive = notes.IsArchive;
                notesEntity.IsPin = notes.IsPin;
                notesEntity.UserId = notes.UserId;
           //   notesEntity.Createat = DateTime.Now;
                //this.context.Notes.Add(notesEntity);
                //int result = this.context.SaveChanges();

                this.context.Notes.Add(notesEntity);  
                this.context.SaveChanges(); 

                if (notesEntity != null)
                {
                    return notesEntity;
                }
                else return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity DeleteNote(long NoteId)
        {
            try
            {

                var deleteNote = context.Notes.FirstOrDefault(x => x.NoteID == NoteId);
                if (deleteNote != null)
                {
                    context.Notes.Remove(deleteNote);
                    context.SaveChanges();
                    return deleteNote;
                }

                return null;


            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity UpdateNote(NoteModel noteModel, long NoteId)
        {
            try
            {
                var update = context.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (update != null)
                {
                    update.Title = noteModel.Title;
                    update.Note = noteModel.Note;
                    update.IsArchive = noteModel.IsArchive;
                    update.Color = noteModel.Color;
                    update.Image = noteModel.Image;
                    update.IsPin = noteModel.IsPin;
                    update.IsTrash = noteModel.IsTrash;
                    context.Notes.Update(update);
                    context.SaveChanges();
                    return update;

                }


                return null;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Pinned(long NoteID)
        {
            try
            {
                var result = context.Notes.Where(r => r.NoteID == NoteID).FirstOrDefault();

                result.IsPin = !result.IsPin;
                context.SaveChanges();
                return result.IsPin;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trashed(long NoteID)
        {
            try
            {
                var result = context.Notes.Where(r => r.NoteID == NoteID).FirstOrDefault();

                result.IsTrash = !result.IsTrash;
                context.SaveChanges();
                return result.IsTrash;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Archieved(long NoteID)
        {
            try
            {
                var result = context.Notes.Where(r => r.NoteID == NoteID).FirstOrDefault();
                result.IsArchive = !result.IsArchive;
                context.SaveChanges();
                return result.IsArchive;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity ColorNote(long NoteId, string color)
        {
            var result = context.Notes.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {

                result.Color = color;
                context.Notes.Update(result);
                context.SaveChanges();
                return result;

            }
            else
            {
                return null;
            }
        }
    }
}
