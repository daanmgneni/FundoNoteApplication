using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class UserRegistration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{8,}$", ErrorMessage = "Invalid password")]
        public string Password { get; set; }
    }
}
