using AplikacjaMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaMVC.Controllers
{
    public class NewController : Controller
    {

        
       
        public IActionResult Index()
        {

            New nazwa = new New();
            nazwa.text = "OMEGALOL";
            ViewBag.name = "mai";
            return View("New");
        }
    }
}
