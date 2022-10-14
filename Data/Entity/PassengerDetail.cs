using System.ComponentModel.DataAnnotations;

namespace DamienATS1.Data.Entity
{
    public class PassengerDetailclass
    {
        [Key]
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

    }
}
