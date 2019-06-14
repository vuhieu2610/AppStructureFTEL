using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common.Utilities
{
    public enum PermissionType
    {
        NoAction = -1,
        View = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
        Import = 4,
        Copy = 5
    }
}
