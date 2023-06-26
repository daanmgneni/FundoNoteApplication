using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using DataLayer.Service;
using DataLayer.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        
        public readonly ICollabBL collabBL;
        public readonly IMemoryCache memoryCache;
        public readonly IDistributedCache distributedCache;
        public readonly FundoContext context;

        public CollaboratorController( ICollabBL collabBL,IMemoryCache memoryCache, FundoContext context,IDistributedCache distributedCache)
        {

            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
        }

    [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(long noteid, long userID, CollabModel model)
        {

            try
            {
                //long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var addresult = collabBL.AddCollaborate(noteid, userID, model);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "New collaborator add sucessfully.", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Not added new collaborator." });
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpDelete]
        [Route("deletecollaborator")]

        public IActionResult deletecollaborator(long collaboratorID)
        {
            var deleteResult = collabBL.DeleteCollaborator(collaboratorID);
            
            if (deleteResult != null)
            {
                return this.Ok(new { sucess = true, msg = "Collaborator Deleted sucessfull", data = deleteResult }); //SSMD form
            }
            else
            {
                return this.BadRequest(new { sucess = false, msg = "Collaborator not Deleted" });
            }

        }
        [HttpGet]
        [Route("ReadAll")]
        public IActionResult ReadAll()
        {
            try
            {

                var res = this.collabBL.GetCollab();
                if (res != null)
                {
                    return this.Ok(new { sucess = true, msg = "collab", data = res }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "No collab" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedCustomerList;
            var CollabList = new List<CollaboratorEntity>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                CollabList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCustomerList);
            }
            else
            {
                CollabList = (List<CollaboratorEntity>)this.collabBL.GetCollab();
                serializedCustomerList = JsonConvert.SerializeObject(CollabList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(CollabList);
        }
    }
}
