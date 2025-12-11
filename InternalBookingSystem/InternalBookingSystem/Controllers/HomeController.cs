using System.Diagnostics;
using InternalBookingSystem.Data;
using InternalBookingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternalBookingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context; 
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext _context)
        {
            _logger = logger;
            this._context = _context;
        }

        public IActionResult Index()
        {
            Switcher();
            return View();
        }



        [NonAction]
        public void Switcher()
        {
            var bookingList = _context.Bookings.ToList();
            var resource = new Resource();

            foreach (var booking in bookingList)
            {
                foreach (var resourceItem in _context.Resources.ToList())
                {
                    if (resourceItem.Id ==booking.ResourcedId)
                    {
                        if (DateTime.Now < Convert.ToDateTime(booking.EndTime))
                        {

                            if (resourceItem != null)
                            {
                                resourceItem.IsAvailable = false;
                                _context.Resources.Update(resourceItem);
                                _context.SaveChanges();

                                Console.WriteLine(resource.IsAvailable.ToString());
                            }

                        }
                    }
                }
            }
            

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
