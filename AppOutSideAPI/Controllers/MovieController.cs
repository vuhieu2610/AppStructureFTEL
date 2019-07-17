using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppOutSideAPI.Business;
using EntityData.Common;
using EntityData.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppOutSideAPI.Controllers
{
    [Route("movie/[action]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        #region initialize
        private MovieBusiness _movieBus;
        private MovieBusiness MovieBusiness
        {
            get
            {
                if (_movieBus == null)
                {
                    _movieBus = new MovieBusiness();
                }
                return _movieBus;
            }
        }
        #endregion
        #region http
        [HttpGet]
        public ActionResult<ReturnResult<Movie>> GetPaging(BaseCondition condition)
        {
            return MovieBusiness.GetPaging(condition);
        }

        [HttpGet]
        public ActionResult<ReturnResult<Movie>> Get(Movie movie)
        {
            return MovieBusiness.GetSingle(movie);
        }

        [HttpGet("{status}")]
        public ActionResult<ReturnResult<Movie>> GetListByStatus(string status)
        {
            return MovieBusiness.GetListByStatus(status);
        }

        [HttpPost]
        public ActionResult<ReturnResult<Movie>> Insert(List<Movie> movieList)
        {
            return MovieBusiness.InsertList(movieList);
        }

        [HttpPut]
        public ActionResult<ReturnResult<Movie>> Update(Movie movie)
        {
            return MovieBusiness.UpdateSingle(movie);
        }

        [HttpPut]
        public ActionResult<ReturnResult<Movie>> Update(List<Movie> movies)
        {
            return MovieBusiness.UpdateList(movies);
        }
        [HttpDelete]
        public ActionResult<ReturnResult<Movie>> Delete(Movie movie)
        {
            return MovieBusiness.DeleteMovie(movie);
        }

        #endregion
    }
}