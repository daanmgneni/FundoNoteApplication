using CommonLayer.Models;
using DataLayer.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IUserDL
    {
        UserEntity Register(UserRegistration user);
        public string Login(UserLogin Login);
        public string ForgetPassword(string EmailId);
        public string ResetPassword(string email, string password, string confirmPassword);

        public List<UserEntity> GetAllUsers();
        public UserEntity GetAllUsersbyID(long userId);
    }
}
