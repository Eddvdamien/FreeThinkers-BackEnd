using DamienATS1.Data.Entity;
using DamienATS1.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DamienATS1.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ATSDbContext BookingLV;
        public BookingController(ATSDbContext BookingCV)
        {
            BookingLV = BookingCV;
        }

        //used by admin
        //to get all Booking
        [HttpGet]
        public async Task<IActionResult> GetAllBooking()
        {
            var allbooking = await BookingLV.BookingT.ToListAsync();
            return Ok(allbooking);
        }

        //used by user
        //to add new Booking
        [HttpPost]
        public async Task<IActionResult> Createbooking([FromBody] BookingDetailsclass createbooking)
        {
            createbooking.BookingID = Guid.NewGuid();
            BookingLV.BookingT.Add(createbooking);
            await BookingLV.SaveChangesAsync();
            return Ok(createbooking);
        }

        //usedby  customer
        //to fetch booking for the particular condition
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Searchbooking([FromRoute] Guid id)
        {
            var fetchbooking = await BookingLV.BookingT.FirstOrDefaultAsync(x => x.BookingID == id);

            if (fetchbooking == null)
                return NotFound(new { message = "Sorry, Booking Not Found!" });
            return Ok(fetchbooking);

        }

        //usedby admin
        //to update booking changes
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, BookingDetailsclass updatebooking)
        {
            var validid = await BookingLV.BookingT.FindAsync(id);
            if (validid == null)
                return NotFound(new { message = "Sorry,Booking Not Found!" });
            validid.Age = updatebooking.Age;
            validid.ArrivalOn = updatebooking.ArrivalOn;
            validid.BPrice = updatebooking.BPrice;
            validid.DepartureOn = updatebooking.DepartureOn;
            validid.EPrice = updatebooking.EPrice;
            validid.Destination = updatebooking.Destination;
            validid.Flight = updatebooking.Flight;
            validid.FlightName = updatebooking.FlightName;
            validid.Gender = updatebooking.Gender;
            validid.PassengerId = updatebooking.PassengerId;
            validid.PassengerName = updatebooking.PassengerName;
            validid.Source = updatebooking.Source;
            return Ok(validid);

        }

        //usedby admin
        //to delete the booking
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var del = await BookingLV.BookingT.FindAsync(id);
            if (del == null)
                return NotFound(new { message = "Sorry, Booking was Not Found!" });
            BookingLV.BookingT.Remove(del);
            await BookingLV.SaveChangesAsync();
            return Ok(del);
        }

        //used by user
        //to get user's booking history and request for cancellation
        [HttpGet]
        [Route("uid")]
        public async Task<IActionResult> BookingbyUN(int userId)
        {
            var id = await BookingLV.BookingT.Where(x => x.PassengerId == userId).ToListAsync();
            if (id == null)
                return NotFound(new { message = "Booking details Not found in Db!" });
            return Ok(id);
        }


    }
}
