using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifaApp
{
    internal class Contact
    {
        public Contact(string email, string mobile)
        {
            Email = email;
            Mobile = mobile;
        }

        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
