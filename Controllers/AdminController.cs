using DamienATS1.Data.Entity;
using DamienATS1.Data.Tables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DamienATS1.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ATSDbContext AdminTLV;
        public AdminController(ATSDbContext AdminTCV)
        {
            AdminTLV = AdminTCV;
        }

        //usedby Admin
        //to get all admins
        [HttpGet]
        public async Task<IActionResult> GetAllAdmin()
        {
            var getadmin = await AdminTLV.AdminT.ToListAsync();
            return Ok(getadmin);
        }

        //only usedby existing admin
        //to create admin account
        [HttpPost]
        public async Task<IActionResult> CreateAdmin([FromBody]Adminclass createadmin)
        {
            createadmin.AdminId = Guid.NewGuid();
            AdminTLV.AdminT.Add(createadmin);
            await AdminTLV.SaveChangesAsync();
            return Ok(createadmin);

        }

        //userdby admin
        //to get admin details by adminid
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetAdminbyId([FromRoute]Guid id)
        {
            var getadminbyId = await AdminTLV.AdminT.FirstOrDefaultAsync(x => x.AdminId == id);
            if (getadminbyId == null)
                return NotFound(new
                {
                    Message = "Admin Details are not found in DB!, unable to fetch"
                });
            return Ok(getadminbyId);

        }

        //usedby admin
        //to update admin details
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAdmin([FromRoute]Guid id, Adminclass updateadmin)
        {
            var checkadmin = await AdminTLV.AdminT.FindAsync(id);
            if (checkadmin == null)
                return NotFound(new { message = "Admin not found in DB!, unable to update" });
            checkadmin.AdminName = updateadmin.AdminName ;
            checkadmin.Age = updateadmin.Age;
            checkadmin.EmailId = updateadmin.EmailId;
            checkadmin.FullName = updateadmin.FullName;
            checkadmin.Gender = updateadmin.Gender;
            checkadmin.Password = updateadmin.Password;
            checkadmin.Phone = updateadmin.Phone;
            await AdminTLV.SaveChangesAsync();
            return Ok(checkadmin);

        }

        //userby admin 
        //to delete the admin account
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAdmin([FromRoute]Guid id)
        {
            var deladmin = await AdminTLV.AdminT.FindAsync(id);
            if (deladmin == null)
                return NotFound(new { message = "admin not found in DB!, unable to delete" });
            AdminTLV.AdminT.Remove(deladmin);
            await AdminTLV.SaveChangesAsync();
            return Ok(deladmin);
        }

        //usedby admin
        //to login verification
        [HttpPost]
        /*routes is needed as passing a list of details in body and
        already made post method without route so api controller will get confuse*/
        [Route("adminlogin")]
        public async Task<IActionResult> AdminLogIn([FromBody]Adminclass admincheck)
        {
            if (admincheck == null)
                return BadRequest();
            var checkadmin = await AdminTLV.AdminT.FirstOrDefaultAsync(x =>
            x.AdminName == admincheck.AdminName && x.Password == admincheck.Password);
            if (checkadmin == null)
                return NotFound(new { message = "Admin is not found in DB!, Enter correct details" });
            return Ok(checkadmin);
        }

        //used ny admin
        //to fetch his creditentials
        [HttpGet]
        [Route("adminname")]
        public async Task<IActionResult> GetadminbyName(string adminname)
        {
            var aname = await AdminTLV.AdminT.FirstOrDefaultAsync(x => x.AdminName == adminname);
            if (aname == null)
                return NotFound(new { message = "Admin is not Found in DB!" });
            return Ok(aname);
        }
        
    }
}
