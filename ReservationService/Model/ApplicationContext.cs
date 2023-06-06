﻿using Microsoft.EntityFrameworkCore;

namespace ReservationService.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public ApplicationContext() { }

        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured)
            {
                return;
            }

            builder.UseSqlServer("Data Source=DESKTOP-P0VP12J;Initial Catalog=Reservation;Integrated Security=true;");
        }
    }
}
