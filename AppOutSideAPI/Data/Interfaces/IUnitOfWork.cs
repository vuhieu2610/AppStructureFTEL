using AppOutSideAPI.Data.Class;
using EntityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        MovieRepository MovieRepository { get; }
    }
}
