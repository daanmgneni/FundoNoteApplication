using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using System;
using System.Data;
using System.Linq;
using System.Security.Claims;

namespace FundoNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;


        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Register(userRegistration);
                if (result != null)
                {
                    logger.LogInformation("Registration Successful");
                    return this.Ok(new { sucess = true, msg = "Registration Sucessfull", data = result });

                }
                else
                {
                    logger.LogWarning("Registration UnSuccessful");
                    return this.BadRequest(new { sucess = false, msg = "Registration UnSucessfull" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Add([FromBody] UserLogin LoginRegistration)
        {
            try
            {
                var reg = this.userBL.Login(LoginRegistration);
             
                if (reg != null)
                {
                    logger.LogInformation("Login Successful");
                    return this.Ok(new { Success = true, message = "Login Sucessfull", Data = reg });
                }
                else
                {
                    logger.LogWarning("Login UnSuccessful");
                    return this.BadRequest(new { Success = false, message = "Login Unsucessfull" });
                }
            }
            catch (System.Exception)
            { 
                throw;
            }
        }

        [HttpPost]
        [Route("ForgetPassWord")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {

                var reg = this.userBL.ForgetPassword(EmailId);
                if (reg != null)

                {
                    logger.LogInformation("Successfully send");
                    return this.Ok(new { Success = true, message = "Token sent Sucessfully please check your mail" ,Data = reg});
                }
                else
                {
                    logger.LogWarning("UnSuccessful to send token to the Mail");
                    return this.BadRequest(new { Success = false, message = "unable to send token to mail" });
                }
            }
            catch (Exception ex)
            {
              
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
              
                var email = User.Claims.FirstOrDefault(e => e.Type == "Email").Value;
                var result = userBL.ResetPassword(email, password, confirmPassword);

               // if (userBL.ResetPassword(email, password, confirmPassword))
                if(result != null)
                {
                    logger.LogInformation("Reset Password Succesfull");
                    return Ok(new { success = true, message = "Password Reset Successful", Data = result });
                }
                else
                {
                    logger.LogWarning("Rest Password Unsucessfull");
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
              
                throw;
            }
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = this.userBL.GetAllUsers();
                if(result != null)
                {
                    logger.LogInformation("Succesfull");
                    return Ok(new { success = true, message = "Successful", Data = result });
                }
                else
                {
                    logger.LogWarning("Unsucessfull");

                    return BadRequest(new { success = false, message = "There are No User Present" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("GetAllUsersbyID")]
        public IActionResult GetAllUsersbyID(long userid)
        {
            try
            {
                var result = this.userBL.GetAllUsersbyID(userid);
                if (result != null)
                {
                    logger.LogInformation("Succesfull");
                    return Ok(new { success = true, message = "Successful", Data = result });
                }
                else
                {
                    logger.LogWarning("Unsucessfull");
                    return BadRequest(new { success = false, message = "There are No User Present" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
