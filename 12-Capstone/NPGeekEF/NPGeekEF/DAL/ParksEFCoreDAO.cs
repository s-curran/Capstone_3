using NPGeekEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.DAL
{
    public class ParksEFCoreDAO : IParksDAO
    {
        private readonly NpGeekContext dbContext;

        public ParksEFCoreDAO(NpGeekContext context)
        {
            dbContext = context;
        }
        public IList<Park> GetAllParks()
        {
            return dbContext.Park.ToList();
        }

        public Park GetParkByCode(string parkCode)
        {
            return dbContext.Park.Where(p => p.ParkCode == parkCode).SingleOrDefault();
        }

        public bool AddNewPark(Park park)
        {
            try
            {
                dbContext.Add(park);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
