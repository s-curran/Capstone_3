using NPGeekEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.DAL
{
    public class SurveyEFCoreDAO : ISurveyDAO
    {
        private readonly NpGeekContext dbContext;

        public SurveyEFCoreDAO(NpGeekContext context)
        {
            dbContext = context;
        }

        public bool AddSurveyResult(SurveyResult survey)
        {
            try
            {
                dbContext.Add(survey);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Dictionary<string, int> GetSurveyResults()
        {
            Dictionary<string, int> results = new Dictionary<string, int>();
            List<SurveyResult> rawSurveys = dbContext.SurveyResult.OrderBy( s => s.ParkCode).ToList();
            foreach (SurveyResult sr in rawSurveys)
            {
                if (results.ContainsKey(sr.ParkCode))
                {
                    results[sr.ParkCode]++;
                }
                else
                {
                    results.Add(sr.ParkCode, 1);
                }
            }

            return results.OrderByDescending( kvp => kvp.Value).ToDictionary( kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}
