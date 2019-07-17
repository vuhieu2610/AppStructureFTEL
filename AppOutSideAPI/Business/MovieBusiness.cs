using AppOutSideAPI.Data.Class;
using EntityData.Common;
using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI.Business
{
    public class MovieBusiness : IDisposable
    {
        // initialize unitOfWork object for transaction purpose
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ReturnResult<Movie> GetPaging(BaseCondition condition)
        {
            return unitOfWork.MovieRepository.GetPaging(condition);
        }

        public ReturnResult<Movie> GetSingle(Movie movie)
        {
            return unitOfWork.MovieRepository.GetSingle(movie);
        }

        public ReturnResult<Movie> GetListByStatus(string status)
        {
            var movieList = unitOfWork.MovieRepository.GetMovieListByStatus(status);
            unitOfWork.Dispose();
            return movieList;
        }

        public ReturnResult<Movie> InsertList(List<Movie> movieList)
        {
            return unitOfWork.MovieRepository.Insert(movieList);
            
        }

        public ReturnResult<Movie> UpdateSingle(Movie movie)
        {
            return unitOfWork.MovieRepository.Update(movie);
        }

        public ReturnResult<Movie> UpdateList(List<Movie> movieList)
        {
            return unitOfWork.MovieRepository.Update(movieList);
        }

        public ReturnResult<Movie> DeleteMovie(Movie movie)
        {
            return unitOfWork.MovieRepository.Delete(movie);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
