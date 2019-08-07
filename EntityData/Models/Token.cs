using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityData.Models
{
    public class Token
    {
        [Required]
        public string AccessToken;
        public long Expire;
        public string RefreshToken = null;
        public string Roles = "admin";
    }
}
