using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTEL.Common
{
    public class ResultObject
    {
        private static readonly ResultObject _success = new ResultObject { Succeeded = true };
        private List<ErrorObject> _errors = new List<ErrorObject>();
        public bool Succeeded { get; protected set; }
        public IEnumerable<ErrorObject> Errors => _errors;
        public static ResultObject Success => _success;

        public static ResultObject Failed(params ErrorObject[] errors)
        {
            var result = new ResultObject { Succeeded = false };
            if (errors != null)
            {
                result._errors.AddRange(errors);
            }
            return result;
        }
        public override string ToString()
        {
            return Succeeded ?
                   "Succeeded" :
                   string.Format("{0} : {1}", "Failed", string.Join(",", Errors.Select(x => x.Code).ToList()));
        }
    }
}
