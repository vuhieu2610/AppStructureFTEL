using EntityData.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppOutSideAPI.Data.Interfaces
{
    public interface IGenericRepository<T>: IDisposable where T : new()
    {
        ReturnResult<T> GetSingle(T item);

        ReturnResult<T> GetPaging(BaseCondition condition);

        ReturnResult<T> Insert(T item);

        ReturnResult<T> Insert(List<T> items);

        ReturnResult<T> Update(T item);

        ReturnResult<T> Update(List<T> items);

        ReturnResult<T> Delete(T item);
    }
}
