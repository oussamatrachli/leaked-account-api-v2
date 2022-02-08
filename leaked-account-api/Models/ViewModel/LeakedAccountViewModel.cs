using System;
using System.Collections.Generic;

namespace LeakedAccountApi.Models.ViewModel
{
    public class LeakedAccountViewModel
    {
        public string Email { get; set; }

        public List<string> Passwords { get; set; }

        public DateTime integrationTime { get; set; }
    }
}
