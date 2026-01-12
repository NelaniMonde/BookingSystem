
using InternalBookingSystem.Data;
using InternalBookingSystem.Models;
using InternalBookingSystem.UserActivityClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternalBookingSystem.Controllers
{
    public class BookingController : Controller
    {
       
        private readonly ApplicationDbContext _context; 
        private readonly IUserActivityLogger _logger;

        public BookingController(ApplicationDbContext _context, 
            IUserActivityLogger _logger)
        {
            this._context = _context;
            this._logger = _logger;
        

        }


        /* view resource event start */
        [Authorize(Roles =SD.Role_User_Admin+", "+SD.Role_User_Manager)]
        public IActionResult ViewResources()
        {
            var equipmentList = _context.Resources.ToList();

            return View(equipmentList);
        }
        /* view resource event end */


        /* Add resource event start */
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager)]
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
           

            if (availability=="on")
            {
                resource.IsAvailable = true;    
            }
            

            _context.Resources.Add(resource);   
            _context.SaveChanges();

            var currentUser = _context.ApplicationUsers.Where(s => s.Email == User.Identity.Name.ToString());
            string employeeId = "";
                

            if (currentUser != null)
            {
                foreach (var item in currentUser)
                {
                    employeeId = item.EmployeeNumber;
                   
                }
            }

            _logger.LogUserActivity(employeeId, "Added a resource/equipment: " + resource.Name.ToString() +
                ". With Location: " + resource.Location.ToString() + ". Capacity of:  " + resource.Capacity,
                User.Identity.Name);

            return RedirectToAction("AddResource");
        }
        /* Add resource event end */


        /*Edit Resource Event Start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager)]
        public IActionResult EditResource(int resourceId)
        {
            var resource = _context.Resources.FirstOrDefault(x => x.Id == resourceId);

            return View(resource);
        }

        [HttpPost]
        public IActionResult EditResource(Resource resource, string availability, int resourceId)
        {
            if (availability == "on")
            {
                resource.IsAvailable = true;
            }

            resource.Id = resourceId;
           
            _context.Resources.Update(resource);
            _context.SaveChanges(); 
            return RedirectToAction("ViewResources");
        }
        /*Edit Resource Event End*/



        /*Delete Resource Event Start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager)]
        public IActionResult DeleteResource(int resourceId) 
        {

            var resource = _context.Resources.FirstOrDefault(x => x.Id == resourceId);

            return View(resource);
        }

        [HttpPost]
        [ActionName("DeleteResource")]
        public IActionResult DeleteResourcePost(int resourceId)
        {
            var resource = _context.Resources.FirstOrDefault(x => x.Id == resourceId);

            if (resource != null)
            {
                _context.Resources.Remove(resource);
                _context.SaveChanges();
            }
            return RedirectToAction("ViewResources");
        }
        /*Delete Resource Event End*/



        /*Booking Event Start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager+", "+SD.Role_User_NormalUser)]
        public IActionResult BookingView(int resourceId)
        {
            var resourcesList = _context.Resources.ToList();
            var bookings = _context.Bookings.ToList();
            var resourceNames = new List<string>();

            if (resourceId > 0)
            {
                var resource= _context.Resources.Find(resourceId);

                if (resource != null)
                {
                    resourceNames.Add(resource.Name);
                }
            }

            else
            {
              
                    foreach (var item in resourcesList)
                    {
                        if (item.IsAvailable == true)
                        {

                            resourceNames.Add(item.Name);
                        }
                    }  

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
        /*Booking Event End*/


        /* View Bookings start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager + ", " + SD.Role_User_NormalUser)]
        public IActionResult ViewBookings()
        {
            var jointTable = new List<BookingsAndResources>();
            var resource = new Resource();

            foreach (var item in _context.Bookings.ToList())
            {

                foreach (var objRec in _context.Resources.ToList())
                {
                    if (item.ResourcedId == objRec.Id)
                    {
                        resource = objRec;
                    }
                }
                    jointTable.Add(new BookingsAndResources
                    {
                        resource = resource,
                        bookings = item
                    });
              }
            
            return View(jointTable);
        }
        /* View Bookings end*/


    }
}
