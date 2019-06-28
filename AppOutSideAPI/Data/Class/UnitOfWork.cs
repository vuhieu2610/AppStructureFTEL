using AppOutSideAPI.Common;
using AppOutSideAPI.Data.Interfaces;
using EntityData;
using EntityData.Common;
using System;

namespace AppOutSideAPI.Data.Class
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProvider _db = new DbProvider();

        public UnitOfWork()
        {
            // ==== Dependency Injection ==== //
            //      ( Tạm thời disable )      //
            //UserRepository = ObjectFactory.Container.With("db").EqualTo(_db).GetInstance<IGenericRepository<Users>>();
            // ============================== //

            UserRepository = new GenericRepository<Users>(_db);
        }

        public UnitOfWork(bool useTransaction)
        {
            //UserRepository = ObjectFactory.Container.With("db").EqualTo(_db).With("useTransaction").EqualTo(useTransaction).GetInstance<IGenericRepository<Users>>();

            UserRepository = new GenericRepository<Users>(_db, true);
        }

        public IGenericRepository<Users> UserRepository { get; } = null;

        public void Dispose()
        {
            UserRepository.Dispose();
            _db.Dispose();
        }
    }
}
