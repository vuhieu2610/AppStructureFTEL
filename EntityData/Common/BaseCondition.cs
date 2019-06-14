using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityData.Common
{
    public class BaseCondition
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int FromRecord => (PageIndex - 1) * PageSize;
        public string IN_WHERE { set; get; }
    }
}
