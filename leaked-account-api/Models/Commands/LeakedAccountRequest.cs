using System.Collections.Generic;

namespace LeakedAccountApi.Models.Commands
{
    public class LeakedAccountRequest
    {
        public string Email { get; set; }

        public List<string> Passwords { get; set; }
    }
}
