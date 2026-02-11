using System;
using System.Collections.Generic;
using System.Text;

namespace LogYourDayAway.Models
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
