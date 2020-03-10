using NPGeekEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPGeekEF.DAL
{
    public interface IParksDAO
    {
        IList<Park> GetAllParks();
    }
}
