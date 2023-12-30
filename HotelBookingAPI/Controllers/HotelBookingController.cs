using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly APIContext _context;

        public HotelBookingController(APIContext context) 
        {
            _context = context;
        }

        //create / edit
        [HttpPost]
        public JsonResult CreatedEdit(HotelBooking hotelBooking)
        {
            if(hotelBooking.Id == 0)
            {
                _context.Bookings.Add(hotelBooking);
            } else
            {
                var bookingInDB = _context.Bookings.Find(hotelBooking.Id);

                if (bookingInDB == null)
                    return new JsonResult(NotFound());

                bookingInDB = hotelBooking;
            }
            _context.SaveChanges();
            return new JsonResult(Ok(hotelBooking));
        }
    }
}
