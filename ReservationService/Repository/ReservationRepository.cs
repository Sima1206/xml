﻿using Microsoft.EntityFrameworkCore;
using ReservationService.Core;
using ReservationService.Model;

namespace ReservationService.Repository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<Reservation> SearchByDate(DateTime startData, DateTime endDate)
        {
            return ApplicationContext.Reservations.Where(x => startData > x.StartDate && endDate < x.EndDate).ToList();
        }




    }
}
