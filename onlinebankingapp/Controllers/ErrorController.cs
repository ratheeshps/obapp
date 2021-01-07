using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace obapp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult unauthorized()
        {
            HttpContext.Session.Clear();
            return View();
        }
    }
}
