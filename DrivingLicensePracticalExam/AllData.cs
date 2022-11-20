using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingLicensePracticalExam
{
    public class AllData
    {
        Response response = new();

        public Dictionary<string, List<string>> GetAllData(string categoryCode)
        {
            var dic = response.GetCity(categoryCode);
            var dicDates = response.GetDates(dic, categoryCode);
            
            return dicDates;
        }

        
    }
}
