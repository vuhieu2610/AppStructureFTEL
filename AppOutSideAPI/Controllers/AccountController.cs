using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityData.Common;
using EntityData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppOutSideAPI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult<ReturnResult<Login>> Login(Login loginModel)
        {

        }
    }
}