using DamienATS1.Data.Entity;
using DamienATS1.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DamienATS1.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ATSDbContext CustomerDBLV;
        public CustomerController(ATSDbContext CustomerDBCV)
        {
            CustomerDBLV = CustomerDBCV;
        }

        //usered only by admin
        //to get all customer registered in the DB         
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var allcustomer = await CustomerDBLV.CustomersT.ToListAsync();
            return Ok(allcustomer);
        }

        //user by Customer
        //to register new customer
        [HttpPost]
        public async Task<IActionResult> CreatingCustomer([FromBody]Customerclass customer)
        {             
            CustomerDBLV.CustomersT.Add(customer);
            await CustomerDBLV.SaveChangesAsync(); //no need to call the customer table 
            return Ok(customer);
        }

        //usedby customer
        //to get customer creditentials to edit
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserbyId([FromRoute]int id)
        {
            var validuser = await CustomerDBLV.CustomersT.FirstOrDefaultAsync(x =>x.UserId==id);
            if (validuser == null)
                return NotFound(new { message = "User not found in DB!, unable to fetch" });
            return Ok(validuser);
        }

        //used by cutomer
        //to update his profile
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute]int id,Customerclass updatecustomer) 
        {
            var customerupdate = await CustomerDBLV.CustomersT.FindAsync(id);
            if (customerupdate == null)
                return NotFound(new
                { Message = "user is not found in Database!" });

            customerupdate.Age = updatecustomer.Age;
            customerupdate.EmailId = updatecustomer.EmailId;
            customerupdate.FullName = updatecustomer.FullName;
            customerupdate.Gender = updatecustomer.Gender;
            customerupdate.Password = updatecustomer.Password;
            customerupdate.Phone = updatecustomer.Phone;
            customerupdate.UserName = updatecustomer.UserName;
            await CustomerDBLV.SaveChangesAsync();
            return Ok(customerupdate);
        }

        //used by Admin 
        //to delete customer from DB
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute]int id)
        {
            var deletecust = await CustomerDBLV.CustomersT.FindAsync(id);
            if (deletecust == null)
                return NotFound(new
                { Message = "user is not found in Database!" });
            CustomerDBLV.CustomersT.Remove(deletecust);
            await CustomerDBLV.SaveChangesAsync();
            return Ok(deletecust);
        }

        //usedby Customer
        //to check his logincreditentials
        /*routes is needed as passing a list of details in body and
        already made post method without route so api controller will get confuse*/
        [HttpPost]
        [Route("logincreditentials")]
        public async Task<IActionResult> CheckCustomer([FromBody] Customerclass usercheck)
        {
            if (usercheck == null) {
                return NotFound(new
                { Message = "Not a Valid User!" }); }
            var checking = await CustomerDBLV.CustomersT.FirstOrDefaultAsync(x =>
            x.UserName == usercheck.UserName && x.Password == usercheck.Password);
            if (checking == null)
            {
                return NotFound(new
                { Message = "user is not found in Database!" }
                    );
            }
            return Ok(usercheck);
        }

        //used by user
        //to get details by username
        [HttpGet]
        [Route("username")]
        public async Task<IActionResult> GetuserbyUName( string username)
        {
            //var uncheck = Convert.ToString(username);
            var checking1 =  await CustomerDBLV.CustomersT.FirstOrDefaultAsync(x=>x.UserName==username);
            if(checking1==null)
                return NotFound(new
                { Message = "user is not found in Database!" }
                  );


            return Ok(checking1);
        }

    }
}
