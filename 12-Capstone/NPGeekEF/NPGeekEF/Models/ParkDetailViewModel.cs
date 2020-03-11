using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.Models
{
    public class ParkDetailViewModel
    {
        public Park Park { get; set; }
        public IDictionary<int, List<string>> TempAlert { get; set; }

        public IDictionary<string, string> Forecast = new Dictionary<string, string>
        {
            { "snow", "Pack snowshoes."},
            { "rain", "Pack rain gear and waterproof shoes."},
            { "thunderstorms", "Seek shelter and avoid hiking on exposed ridges." },
            { "sun", "Pack sunblock." }
        };
        public void SetAlert(Park park)
        {
            TempAlert = new Dictionary<int, List<string>>();
            foreach (Weather day in park.Weather)
            {
                
                List<string> alerts = new List<string>();
                if (day.High > 75)
                {
                    alerts.Add("Bring an extra gallon of water.");
                }
                if (day.Low < 20)
                {
                    alerts.Add("Beware of exposure to frigid temperatures.");
                }
                if ((day.High - day.Low) > 20)
                {
                    alerts.Add("Wear breathable layers.");
                }
                TempAlert[day.FiveDayForecastValue] = alerts;
            }
        }
    }
}
