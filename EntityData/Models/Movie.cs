using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityData.Models
{
    public class Movie
    {
        public int PK_id { get; set; }
        public string Name { get; set; }
        public string Ava_url { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string Rated { get; set; }
        public int Length { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime Released_date { get; set; }
    }
}
