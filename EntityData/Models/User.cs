using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityData
{
    public class User
    {
        public int Id { get; set; }
        public string AvatarUrl { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHashed { get; set; }
        public string Email { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public int Phone { get; set; }
        public DateTime BecameCustomer { get; set; }
        public string EmailConfirmed { get; set; }
        public int AmountPay { get; set; }
        public int FK_AdressConfigId { get; set; }
        public string Role { get; set; }
    }
}
