
using Elfie.Serialization;
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

        /*Remove SD.Role_User_NormalUser after testing*/
        /* view resource event start */
        [Authorize(Roles =SD.Role_User_Admin+", "+SD.Role_User_Manager)]
        public IActionResult ViewResources()
        {
            var equipmentList = _context.Resources.ToList();

            return View(equipmentList);
        }
        /* view resource event end */


        /*Remove SD.Role_User_NormalUser after testing*/
        /* Add resource event start */
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager )]
        public IActionResult AddResource(string message)
        {

            TempData["Success"]=message;
            
            return View();
        }

        [HttpPost]
        public IActionResult AddResource(Models.Resource resource, string availability)
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

            var successMessage = "A resource/equipment: " + resource.Name.ToString() +
                ". With Location: " + resource.Location.ToString() + ". Capacity of:  " 
                + resource.Capacity + " has been added Successfully!!!";

            return RedirectToAction("AddResource", new { message = successMessage});
        }
        /* Add resource event end */


        /*Remove SD.Role_User_NormalUser after testing*/
        /*Edit Resource Event Start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager )]
        public IActionResult EditResource(int resourceId, string message)
        {
            var resource = _context.Resources.FirstOrDefault(x => x.Id == resourceId);

            TempData["Success"]=message;

            return View(resource);
        }

        [HttpPost]
        public IActionResult EditResource(Models.Resource resource, string availability, int resourceId)
        {
            if (availability == "on")
            {
                resource.IsAvailable = true;
            }

            resource.Id = resourceId;
           
            _context.Resources.Update(resource);
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



            _logger.LogUserActivity(employeeId, "Edited a resource/equipment: " + resource.Name.ToString() +
               ". With Location: " + resource.Location.ToString() + ". Capacity of:  " + resource.Capacity,
               User.Identity.Name);

            var successMessage = "A resource/equipment: " + resource.Name.ToString() +
                ". With Location: " + resource.Location.ToString() + ". Capacity of:  "
                + resource.Capacity + " has been Edited Successfully!!!";


            return RedirectToAction("ViewResources", new {message =successMessage});
        }
        /*Edit Resource Event End*/


        /*Remove SD.Role_User_NormalUser after testing*/
        /*Delete Resource Event Start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager )]
        public IActionResult DeleteResource(int resourceId, string message) 
        {

            var resource = _context.Resources.FirstOrDefault(x => x.Id == resourceId);

            //We are checking to see if the string has anything
            if (message != null)
            {
                if (message.Length > 1)
                {
                    TempData["Warning"] = message;
                }
            }
            return View(resource);
        }

        [HttpPost]
        [ActionName("DeleteResource")]
        public IActionResult DeleteResourcePost(int resourceId)
        {
            var resource = _context.Resources.FirstOrDefault(x => x.Id == resourceId);
            //The obvious you cant assign a null to var variable
            //why make it a var instead of a string? its a habit at this point 
            var warningMessage = "";
            if (resource != null)
            {
                _context.Resources.Remove(resource);
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


                   _logger.LogUserActivity(employeeId, "Deleted a resource/equipment: " + resource.Name.ToString() +
                   ". With Location: " + resource.Location.ToString() + ". Capacity of:  " + resource.Capacity,
                   User.Identity.Name);

                warningMessage = "A resource/equipment: " + resource.Name.ToString() +
                    ". With Location: " + resource.Location.ToString() + ". Capacity of:  "
                    + resource.Capacity + " has been deleted Successfully!!!";
            }
            return RedirectToAction("ViewResources", new {message=warningMessage});
        }
        /*Delete Resource Event End*/



        /*Booking Event Start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager+", "+SD.Role_User_NormalUser)]
        public IActionResult BookingView(int resourceId, string message)
        {
            Switcher();
            
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
            TempData["Success"] = message;
            return View();
        }

      

        [HttpPost]
        public IActionResult BookingView(Booking booking,string resourceName)
        {
            var resourcesList = _context.Resources.Where(rs => rs .Name == resourceName).ToList();

            booking.ResourcedId = resourcesList[0].Id;

            _context.Bookings.Add(booking);

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


            _logger.LogUserActivity(employeeId, "Created a booking for resource/equipment: " + resourceName +
            ". With Start Date: " + booking.StartTime + ". End Date:  " + booking.EndTime+
            ". Booked By: "+booking.BookedBy+". Purpose: "+booking.Purpose,
            User.Identity.Name);

        var   successMessage = "A booking for resource/equipment: " + resourceName +
            ". With Start Date: " + booking.StartTime + ". End Date:  " + booking.EndTime +
            ". Booked By: " + booking.BookedBy + ". Purpose: " + booking.Purpose + " has been created Successfully!!!";


            return RedirectToAction("BookingView", new {message=successMessage});
        }
        /*Booking Event End*/


        /* View Bookings start*/
        [Authorize(Roles = SD.Role_User_Admin + ", " + SD.Role_User_Manager + ", " + SD.Role_User_NormalUser)]
        public IActionResult ViewBookings()
        {
            var jointTable = new List<BookingsAndResources>();
            var resource = new Models.Resource ();

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









        public void Switcher()
        {
            var bookingList = _context.Bookings.ToList();
            var resource = new Models.Resource();

            foreach (var booking in bookingList)
            {
                foreach (var resourceItem in _context.Resources.ToList())
                {
                    if (resourceItem.Id == booking.ResourcedId)
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

                        if (DateTime.Now > Convert.ToDateTime(booking.EndTime))
                        {
                            resourceItem.IsAvailable = true;
                            _context.Resources.Update(resourceItem);
                            _context.SaveChanges();
                        }
                    }
                    /* Conditional Statement because deciding an equipment 
                     * should be available or not, can be  or should up to the admin*/
                    //else if (resourceItem.Id == booking.ResourcedId)
                    //{
                    //    resourceItem.IsAvailable = true;
                    //    _context.Resources.Update(resourceItem);
                    //    _context.SaveChanges();
                    //}
                }
            }


        }
    }
}
