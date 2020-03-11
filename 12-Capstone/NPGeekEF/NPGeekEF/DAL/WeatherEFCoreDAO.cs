using NPGeekEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.DAL
{
    public class WeatherEFCoreDAO : IWeatherDAO
    {
        private readonly NpGeekContext dbContext;

        public WeatherEFCoreDAO(NpGeekContext context)
        {
            dbContext = context;
        }

        public IList<Weather> GetWeatherByParkCode(string parkCode)
        {
            return dbContext.Weather.Where(w => w.ParkCode == parkCode).ToList();
        }
    }
}
