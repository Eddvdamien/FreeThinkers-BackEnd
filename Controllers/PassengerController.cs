using DamienATS1.Data.Entity;
using DamienATS1.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DamienATS1.Controllers
{
    [Route("api/passenger")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly ATSDbContext PassengerLV;
        public PassengerController(ATSDbContext PassengerCV)
        {
            PassengerLV = PassengerCV;
        }

        //to get all passenger list
        //flying in different flight
        [HttpGet]
        public async Task<IActionResult> GetAllPassenger()
        {
            var allpassenger=await PassengerLV.PassengerT.ToListAsync();
            return Ok(allpassenger);
        }

        //usedby customer while booking
        //to add passengers
        [HttpPost]
        public async Task<IActionResult> CreatePassenger([FromBody]PassengerDetailclass createpassenger)
        {
            await PassengerLV.PassengerT.AddAsync(createpassenger);
            await PassengerLV.SaveChangesAsync();
            return Ok(createpassenger);
        }

        //to get passengerby Id
        //used in filling bookingtable
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetPassengerbyId([FromRoute]int id)
        {
            var validid = await PassengerLV.PassengerT.FirstOrDefaultAsync(x => x.PassengerId == id);
            if (validid == null)
                return NotFound(new { message = "Passenger is not in DB!" });
            return Ok(validid);

        }

        //Usedby by Admin
        //to update the error in Passengetable
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdatePassenger([FromRoute]int id, PassengerDetailclass updatepassenger)
        {
            var validid = await PassengerLV.PassengerT.FindAsync(id);
            if (validid == null)
                return NotFound(new { message = "Passenger is not in DB!" });
            validid.Age = updatepassenger.Age;
            validid.Gender = updatepassenger.Gender;
            validid.PassengerName = updatepassenger.PassengerName;
            await PassengerLV.SaveChangesAsync();
            return Ok(validid);

        }

        //usedby admin
        //to delete the passenger list after flight lands if needed
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeletePassenger([FromRoute]int id)
        {
            var validid = await PassengerLV.PassengerT.FindAsync(id);
            if (validid == null)
                return NotFound(new { message = "Passenger is not in DB!" });
            PassengerLV.PassengerT.Remove(validid);
            await PassengerLV.SaveChangesAsync();
            return Ok(validid);

        }

    }
}
