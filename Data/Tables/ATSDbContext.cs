using DamienATS1.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace DamienATS1.Data.Tables
{
    public class ATSDbContext:DbContext
    {
        public ATSDbContext(DbContextOptions<ATSDbContext> options): base(options)
        {
                
        }

        public DbSet<Customerclass> CustomersT { get; set; }
        public DbSet<Adminclass> AdminT { get; set; }
        public DbSet<PassengerDetailclass> PassengerT { get; set; }
        public DbSet<FlightDetailsclass> FlightT { get; set; }
        public DbSet<BookingDetailsclass> BookingT { get; set; }

    }

}
