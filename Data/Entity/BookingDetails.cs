using System.ComponentModel.DataAnnotations;

namespace DamienATS1.Data.Entity
{
    public class BookingDetailsclass
    {
        [Key]
        public Guid BookingID { get; set; }
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int Flight { get; set; }
        public string FlightName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureOn { get; set; }
        public DateTime ArrivalOn { get; set; }
        public int EPrice { get; set; }
        public int BPrice { get; set; }
    }
}
