
using InternalBookingSystem.Data;
using InternalBookingSystem.Models;
using InternalBookingSystem.UserActivityClasses;
using Microsoft.AspNetCore.Mvc;

namespace InternalBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context; 
        private readonly IUserActivityLogger _logger;

        public BookingController(ApplicationDbContext _context, IUserActivityLogger _logger)
        {
            this._context = _context;
            this._logger = _logger; 
            
        }



        public IActionResult BookingView()
        {
            return View();
        }


        public IActionResult AddResource()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddResource(Resource resource, string availability)
        {
            return View();
        }

        public IActionResult EditResource()
        {
            return View();
        }


        public IActionResult DeleteResource() 
        {
            return View();
        }
        
        public IActionResult ViewResources()
        {
            return View();
        }
    }
}
