using DamienATS1.Data.Entity;
using DamienATS1.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DamienATS1.Controllers
{
    [Route("api/flight")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly ATSDbContext FlightLV;
        public FlightController(ATSDbContext FlightCV)
        {
            FlightLV = FlightCV;
        }

        //used by admin
        //to get all flight
        [HttpGet]
        public async Task<IActionResult> GetAllFight()
        {
            var allflight = await FlightLV.FlightT.ToListAsync();
            return Ok(allflight);
        }

        //used by admin
        //to add new flights
        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightDetailsclass createflight)
        {
            FlightLV.FlightT.Add(createflight);
            await FlightLV.SaveChangesAsync();
            return Ok(createflight);
        }

        //used by admin 
        //to edit flight details or schedule
        [HttpGet]
        [Route("{id:int}")]
        public async  Task<IActionResult> GetFlightbyId([FromRoute] int id)
        {
            var flight = await FlightLV.FlightT.FirstOrDefaultAsync(x => x.Flight == id);
            if (flight == null)
            {
                return NotFound(new { message = "Sorry, Flight is not forus in DB!" });
            }
            return Ok(flight);
        }

        //usedby  customer
        //to fetch all the flight for the particular condition
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchFlight( string source, string destination, DateTime dd)
        {
            var fetchflight = await FlightLV.FlightT.Where(x =>
            (
                x.Source.ToLower() == source && x.Destination.ToLower() == destination 
                && x.DepartureOn >= dd

            )).ToListAsync();
            if (fetchflight == null)
                return NotFound(new { message = "Sorry, Flight Not Found!" });
            return Ok(fetchflight);
            // && x.DepartureOn.ToShortDateString() == departuredate
         

        }

        //usedby admin
        //to update flight changes
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateFlight([FromRoute]int id, FlightDetailsclass updateflight)
        {
            var validid = await FlightLV.FlightT.FindAsync(id);
            if(validid==null)
                return NotFound(new { message = "Sorry, Flight Not Found!" });
            validid.ArrivalOn = updateflight.ArrivalOn;
            validid.BPrice = updateflight.BPrice;
            validid.DepartureOn = updateflight.DepartureOn;
            validid.Destination = updateflight.Destination;
            validid.EPrice = updateflight.EPrice;
            validid.FlightName = updateflight.FlightName;
            //validid.ArrivalTi = updateflight.ArrivalTime;
            //validid.DepartureTime = updateflight.DepartureTime;
            validid.Source = updateflight.Source;
            await FlightLV.SaveChangesAsync();
            return Ok(validid);

        }

        //usedby admin
        //to delete the scheduled flight
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteFlight([FromRoute]int id)
        {
            var del = await FlightLV.FlightT.FindAsync(id);
            if(del==null)
                return NotFound(new { message = "Sorry, Flight Not Found!" });
            FlightLV.FlightT.Remove(del);
            await FlightLV.SaveChangesAsync();
            return Ok(del);
        }

        //to get flight by id but searching with a body
        [HttpGet]
        [Route("grft")]
        public async Task<IActionResult> GetFlightbyIdandBody([FromRoute] int id,FlightDetailsclass sf)
        {
            var flight = await FlightLV.FlightT.FirstOrDefaultAsync(x => x.Flight != id);
            if (flight.Source == sf.Source && flight.Destination==sf.Destination && flight.DepartureOn>= sf.DepartureOn)
            {
                return NotFound(new { message = "Sorry, Flight is not forus in DB!" });
            }
            return Ok(flight);
        }
    }
}
