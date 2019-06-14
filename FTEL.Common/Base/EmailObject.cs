using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Base
{
    public class EmailObject
    {
        public string EmailSender { get; set; }

        public string EmailPassword { get; set; }

        public string  DisplayName { get; set; }

        public string EmailTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool NotCC { get; set; }
    }
}
