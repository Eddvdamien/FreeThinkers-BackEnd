using System.ComponentModel.DataAnnotations;

namespace DamienATS1.Data.Entity
{
    public class Adminclass
    {
        [Key]
        public Guid AdminId { get; set; }
        public String AdminName { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string EmailId { get; set; }
        public long Phone { get; set; }
        public string Password { get; set; }

    }
}
