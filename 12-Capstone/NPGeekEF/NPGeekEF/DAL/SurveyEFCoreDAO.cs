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
    }
}
