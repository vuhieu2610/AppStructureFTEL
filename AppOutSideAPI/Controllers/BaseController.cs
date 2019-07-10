using Microsoft.AspNetCore.Mvc;
using System;

namespace AppOutSideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController<T> : ControllerBase, IDisposable where T : class
    {
        protected T _bus = null;

        public void Dispose()
        {
            _bus = default(T);
        }
    }
}