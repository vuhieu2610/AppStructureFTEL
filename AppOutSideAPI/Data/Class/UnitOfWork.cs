using AppOutSideAPI.Data.Interfaces;
using EntityData;
using EntityData.Common;

namespace AppOutSideAPI.Data.Class
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbProvider _db = null;
        public UnitOfWork()
        {
            _db = new DbProvider();

        }

        public IGenericRepository<Users> UserRepository => new GenericRepository<Users>(_db);
    }
}
