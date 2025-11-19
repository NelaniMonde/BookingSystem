
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



      


        public IActionResult AddResource()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddResource(Resource resource, string availability)
        {
            /*so if availability is set to on then set the isAvailable to true
             and if availability is set to null then set it to false
             */

            if(availability=="on")
            {
                resource.IsAvailable = true;    
            }
            

            _context.Resources.Add(resource);   
            _context.SaveChanges();
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


        public IActionResult BookingView()
        {
            var resourcesList = _context.Resources.ToList();
            var resourceNames = new List<string>();

            foreach (var item in resourcesList)
            {
                resourceNames.Add(item.Name);
            }

            ViewBag.ResourceNameList=resourceNames;

            return View();
        }


        [HttpPost]
        public IActionResult BookingView(Booking booking,string resourceName)
        {
            var resourcesList = _context.Resources.Where(rs => rs .Name == resourceName).ToList();

            booking.ResourcedId = resourcesList[0].Id;

            _context.Bookings.Add(booking);

            _context.SaveChanges();

            

            return RedirectToAction("BookingView");
        }



    }
}
