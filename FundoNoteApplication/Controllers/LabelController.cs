using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using DataLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        public readonly ILabelBL labelBL;
        public readonly IMemoryCache memoryCache;
        public readonly IDistributedCache distributedCache;
        public readonly FundoContext context;
        public LabelController(ILabelBL labelBL,IMemoryCache memoryCache,FundoContext context,IDistributedCache distributedCache)
        {

            this.labelBL = labelBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
        }

        [HttpPost]
        [Route("AddLabel")]
        public IActionResult AddLabel(long noteid , long userid, LableModel model)
        {

            try
            { 
                var addresult = labelBL.CreateLable(noteid, userid, model);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "Label Added", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "Label Not Added" });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        [Route("ReadAll")]
        public IActionResult ReadAll()
        {
            try
            {

                var res = this.labelBL.GetAllLable();
                if (res != null)
                {
                    return this.Ok(new { sucess = true, msg = "Label", data = res }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "No Lables" });
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
            var cacheKey = "LabelList";
            string serializedCustomerList;
            var LabelList = new List<LabelEntity>();
            var redisCustomerList = await distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedCustomerList);
            }
            else
            {
                // customerList = await context.Notes.ToListAsync();
                LabelList = (List<LabelEntity>)this.labelBL.GetAllLable();
                serializedCustomerList = JsonConvert.SerializeObject(LabelList);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return Ok(LabelList);
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLable(long lableId, UpdateLabelModel model)
        {
            try
            {
                var lable = labelBL.UpdateLable(lableId, model);
                if (lable != null)
                {
                    
                    return Ok(new { Success = true, message = "Lable Updated Sucessfully", data = lable });

                }
                else
                {
                    
                    return BadRequest(new { Success = false, message = "No Lable found with LableId" });

                }



            }
            catch (Exception)
            {
                
                throw;
            }

        }
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long lableId)
        {
            try
            {
                var addresult = labelBL.DeleteLable(lableId);
                if (addresult != null)
                {
                    return this.Ok(new { sucess = true, msg = "label Deleted sucessfull", data = addresult }); //SSMD form
                }
                else
                {
                    return this.BadRequest(new { sucess = false, msg = "label not Deleted" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
