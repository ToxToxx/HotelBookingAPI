using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public JsonResult CreateAndEdit(HotelBooking hotelBooking)
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

        //Get
        [HttpGet]
        public JsonResult GetBooking(int id)
        {
            var resultBooking = _context.Bookings.Find(id);

            if(resultBooking == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(resultBooking));
        }

        //Delete
        [HttpDelete]
        public JsonResult DeleteBooking(int id)
        {
            var resultBooking = _context.Bookings.Find(id);

            if (resultBooking == null)
                return new JsonResult(NotFound());

            _context.Bookings.Remove(resultBooking);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        //Get All
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Bookings.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
