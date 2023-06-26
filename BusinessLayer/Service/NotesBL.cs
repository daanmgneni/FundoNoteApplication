using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using DataLayer.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {

        private readonly INotesDL NotesDL;

        public NotesBL(INotesDL NotesDL)
        {
            this.NotesDL = NotesDL;


        }
        public bool CheckUserId(long userID)
        {
            try
            {
               return this.NotesDL.CheckUserId(userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity AddNote(NoteModel note)
        {
            try
            {
                return this.NotesDL.AddNote(note);
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
                return this.NotesDL.DeleteNote(NoteId);
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
                return this.NotesDL.UpdateNote(noteModel, NoteId);
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
                return this.NotesDL.Pinned(NoteID);
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
                return this.NotesDL.Trashed(NoteID);
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
                return this.NotesDL.Archieved(NoteID);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity ColorNote(long NoteId, string color)
        {
            try
            {
                return this.NotesDL.ColorNote(NoteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Imaged(long NoteID, IFormFile image)
        {
            try
            {
                return this.NotesDL.Imaged(NoteID, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NotesEntity> Search(string query)
        {
            try
            {
                return this.NotesDL.Search(query);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<NotesEntity> GetAllNote()
        {
            try
            {
                return this.NotesDL.GetAllNote();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
       
    }


