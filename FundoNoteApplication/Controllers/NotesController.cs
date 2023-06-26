using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;
using DataLayer.DB;
using DataLayer.Service;
using DataLayer.Interface;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL NoteBL;
        private readonly FundoContext context;
        public readonly IMemoryCache memoryCache;
        public readonly IDistributedCache distributedCache;
        public NotesController(INotesBL NoteBL,FundoContext context, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.NoteBL = NoteBL;
            this.context = context;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [HttpPost("Add")]
        public IActionResult AddNotes(NoteModel addnote )
        {
            try
            {
                var UID = addnote.UserId;
                var check = NoteBL.CheckUserId(UID);
                if (check != true)
                {
                    return this.BadRequest(new { sucess = false, msg = "Not Created" });
                }
                var addresult = NoteBL.AddNote(addnote);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Note add sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Note not Created" });
                }

            } 
            catch (System.Exception)
            {

                throw;
            }

        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            { 
                var addresult = NoteBL.DeleteNote(NoteId);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Note Deleted sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Note not Deleted" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPost("Update")]
        public IActionResult UpdateNotes(NoteModel noteModel,long noteID)
        {
            try
            {
              //  long userID = Convert.ToInt32(User.Claims.FirstOrDefault(user => user.Type == "userID").Value);
                var addresult = NoteBL.UpdateNote(noteModel, noteID);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Note updated sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Note not Deleted" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveNote(long NoteId, long userid)
        {
            try
            {
                var result = NoteBL.Archieved(NoteId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Archived Successfully", data = result });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Unarchived", data = result });
                }

                else
                {
                    return this.BadRequest(new { Success = false, Message = "Archived unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long NoteId)
        {
            try
            {
                
                var result = NoteBL.Pinned(NoteId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Note Pinned Successfully", data = result });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Unpinned", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note Pinned Unsuccessfully" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long NotesId)
        {
            try
            {
                
                var result = NoteBL.Trashed(NotesId);
                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Trashed Successfully", data = result });
                }
                else if (result == false)
                {
                    return this.Ok(new { Success = true, message = "Untrashed", data = result });
                }

                else
                {

                    return this.BadRequest(new { Success = false, message = "Trashed unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        [Route("Color")]
        public IActionResult ColourNote(long NoteId, string color)
        {
            try
            {
              
                var colors = NoteBL.ColorNote(NoteId, color);
                if (colors != null)
                {

                    return Ok(new { Success = true, message = "Added Colour Successfully", data = colors });
                }
                else
                {

                    return BadRequest(new { Success = false, message = "Added Colour Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Imaged")]
        public IActionResult Imaged(long noteId, IFormFile image)
        {
            try
            {
             
                var result = NoteBL.Imaged(noteId, image);
                if (result != null)
                {
          
                    return Ok(new { Status = true, Message = "Image Uploaded Successfully"});
                }
                else
                {
     
                    return BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessfully", Data = result });
                }
            }
            catch (Exception)
            {
      
                throw;
            }
        }
        [HttpGet]
        [Route("SearchNotes")]
        public IActionResult SearchNotes(string query)
        {
            try
            {
                var result = this.NoteBL.Search(query);
                var count = result.Count();
               
                if (result != null)
                {
                    return Ok(new { success = true, message = "Data is Present in the table",count ,Data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "No Data Present" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("SearchNotesbyPage")]
        public IActionResult SearchNotesbyPage(string query,int page = 1, int pagesize = 10)
        {
            try
            {
                var result = this.NoteBL.Search(query);
                var count = result.Count();
                var pageinatNotes = result.Skip((page-1)*pagesize).Take(pagesize);
                var totalpages = (int)Math.Ceiling((double)count/pagesize);

                if (result != null)
                {
                    return Ok(new { success = true, message = "Data is Present in the table", count,pageinatNotes,totalpages});
                }
                else
                {
                    return BadRequest(new { success = false, message = "No Data Present" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("GetAllNote")]
        public IActionResult GetAllNote()
        {
            try
            {
                List<NotesEntity> result = this.NoteBL.GetAllNote();
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Note got Successfully", data = result });
                }
                else
                    return this.BadRequest(new { Success = false, message = "Note not Available" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedCustomerList;
            var NoteList = new List<NotesEntity>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                NoteList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedCustomerList);
            }
            else
            {
               NoteList = (List<NotesEntity>)this.NoteBL.GetAllNote();
                serializedCustomerList = JsonConvert.SerializeObject(NoteList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(NoteList);
        }
    }
}

