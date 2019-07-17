using EntityData.Common;
using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI.Data.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        ReturnResult<Movie> GetMovieListByStatus(string status);
    }
}
