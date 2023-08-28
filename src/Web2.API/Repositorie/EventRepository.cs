﻿using Microsoft.Extensions.Logging;
using Web2.API.Data;
using Web2.API.Data.Models;

namespace Web2.API.Repositorie
{
    public class EventRepository : IEventRepository
    {
        private readonly TP2A_Context _context;

        public EventRepository(TP2A_Context context)
        {
            _context = context;
        }

        public Evenement GetById(int eventId)
        {
            return _context.Evenements.Find(eventId);
        }

        public decimal TotalDesVentesEvenement(int eventId, int nombreDePlace)
        {
            var evenement = GetById(eventId);
            if (evenement == null)
            {
                throw new ArgumentException("Invalid event ID");
            }

            decimal totalSales = (decimal)(evenement.Prix * nombreDePlace);
            return totalSales;
        }

        // Autres implémentations de méthodes
    }
}
