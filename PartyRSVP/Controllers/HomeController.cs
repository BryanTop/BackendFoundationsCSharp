using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartyRSVP.Models;

namespace PartyRSVP.Controllers
{
    public class HomeController : Controller
    {
       public ViewResult Index(){
           return View("Rsvp");
       }

       [HttpPost]
       public ViewResult RsvpForm(GuestResponse guestResponse) {
           return View("Thanks", guestResponse);
       } 
    }
}
