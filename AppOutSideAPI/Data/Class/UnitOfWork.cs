using AppOutSideAPI.Common;
using AppOutSideAPI.Data.Interfaces;
using EntityData;
using EntityData.Common;
using EntityData.Models;
using System;

namespace AppOutSideAPI.Data.Class
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProvider _db = null;
        private MovieRepository _movieRepository = null;
        private GenericRepository<City> _cityRepository = null;

        public MovieRepository MovieRepository
        {
            get
            {
                if (_movieRepository == null && _db != null)
                {
                    _movieRepository = new MovieRepository(_db);    
                }
                return _movieRepository;
            }
        }

        public GenericRepository<City> CityRepository
        {
            get
            {
                if(_cityRepository == null && _db != null)
                {
                    _cityRepository = new GenericRepository<City>(_db);
                }
                return _cityRepository;
            }
        }

        public UnitOfWork()
        {
            _db = new DbProvider();
            if (_db == null)
            {
                throw new ArgumentException("DbProvider is null");
            }
        }

        public void Dispose()
        {
            MovieRepository.Dispose();
            _db.Dispose();
        }
    }
}
