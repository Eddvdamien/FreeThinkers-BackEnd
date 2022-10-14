using System.ComponentModel.DataAnnotations;

namespace DamienATS1.Data.Entity
{
    public class Customerclass
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }


    }
}
