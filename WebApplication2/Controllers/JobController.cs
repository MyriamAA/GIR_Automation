﻿using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Download()
        {
            return View();
        }
    }
}
