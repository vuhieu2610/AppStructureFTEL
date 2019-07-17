using AppOutSideAPI.Data.Interfaces;
using EntityData.Common;
using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI.Data.Class
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly DbProvider _db = null;
        public MovieRepository(DbProvider db) : base(db)
        {
            this._db = db;
        }

        public ReturnResult<Movie> GetMovieListByStatus(string status)
        {
            string errorMsg = string.Empty;
            string errorCode = string.Empty;
            int outTotalRow = 0;
            List<Movie> movieList = new List<Movie>();
            _db.SetQuery("MOVIE_GET_LIST_BY_STATUS", CommandType.StoredProcedure)
               .SetParameter("STATUS", SqlDbType.NVarChar, status, 20, ParameterDirection.Input)
               .SetParameter("OUT_TOTAL_COUNT", SqlDbType.Int, DBNull.Value, 4000, ParameterDirection.Output)
               .SetParameter("ERROR_CODE", SqlDbType.NVarChar, errorCode, 10, ParameterDirection.Input)
               .SetParameter("ERROR_MSG", SqlDbType.NVarChar, errorMsg, 1000, ParameterDirection.Input)
               .GetList<Movie>(out movieList)
               .Complete();

            _db.GetOutValue("OUT_TOTAL_COUNT", out outTotalRow)
                .GetOutValue("ERROR_CODE", out errorCode)
                .GetOutValue("ERROR_MSG", out errorMsg);

            return new ReturnResult<Movie>()
            {
                ListItem = movieList,
                Code = errorCode,
                Message = errorMsg,
                TotalRow = outTotalRow
            };
        }

    }
}
