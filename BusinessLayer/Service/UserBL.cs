using BusinessLayer.Interface;
using CommonLayer.Models;
using DataLayer.DB;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserDL userDL;
        public UserBL(IUserDL userDL)
        {
            this.userDL = userDL;
        }

        public UserEntity Register(UserRegistration user)
        {
            try
            {
                return userDL.Register(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin Login)
        {

            try
            {
                return this.userDL.Login(Login);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ForgetPassword(string EmailId)
        {

            try
            {
                return this.userDL.ForgetPassword(EmailId);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                return this.userDL.ResetPassword(email, password, confirmPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<UserEntity> GetAllUsers()
        {
            try
            {
                return this.userDL.GetAllUsers();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity GetAllUsersbyID(long userId)
        {
            try
            {
                return this.userDL.GetAllUsersbyID(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
