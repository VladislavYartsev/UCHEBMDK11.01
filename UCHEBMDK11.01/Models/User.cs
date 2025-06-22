using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCHEBMDK11._01.Models
{
    public class User
    {
        private int i = 0;
        public int id { get; set; }
        public string name { get; set; }
        public string password { get; set; }

        public int role_id { get; set; }

        public User(string name, string password, int role_id)
        {
            id = i;
            this.name = name;
            this.password = password;
            this.role_id = role_id;
            i++;
        }
    }
}
