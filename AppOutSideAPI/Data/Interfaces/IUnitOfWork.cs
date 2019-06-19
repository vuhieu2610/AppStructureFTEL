using EntityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Users> UserRepository { get; }
    }
}
