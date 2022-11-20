using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DrivingLicensePracticalExam;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Globalization;
// 3 = mehanica 
// 4 = avtomatika
namespace DrivingLicensePracticalExam
{
    public class Response
    {
        Browser Browser = new Browser();

        public Dictionary<int, string> GetCity(string categoryCode)
        {
            Dictionary<int, string> Center = new();

            string urlCenterId = $"https://api-my.sa.gov.ge/api/v1/DrivingLicensePracticalExams2/DrivingLicenseExamsCenters2?CategoryCode={categoryCode}";
            HttpResponseMessage responseCenterId = Browser.UrlGetToHttp(urlCenterId).Result;
            var contentCenterId = responseCenterId.Content.ReadAsStringAsync().Result;
            List<CenterId> deserializeCenterId = JsonConvert.DeserializeObject<List<CenterId>>(contentCenterId);

            foreach (CenterId center in deserializeCenterId)
            {
                Center[center.serviceCenterId] = Unidecode.NET.Unidecoder.Unidecode(center.serviceCenterName).Replace("`", "") + ":";
            }

            return Center;
        }

        public List<Date> GetDeserializeDate(KeyValuePair<int, string> center, string categoryCode)
        {
            string urlDate = $"https://api-my.sa.gov.ge/api/v1/DrivingLicensePracticalExams2/DrivingLicenseExamsDates2?CategoryCode={categoryCode}&CenterId={center.Key}";
            HttpResponseMessage responseDate = Browser.UrlGetToHttp(urlDate).Result;
            var contentDate = responseDate.Content.ReadAsStringAsync().Result;

            List<Date> deserializeDate = JsonConvert.DeserializeObject<List<Date>>(contentDate);

            return deserializeDate;
        }
     
        
        public Dictionary<string, List<string>> GetDates(Dictionary<int, string> Center, string categoryCode)
        {
            Dictionary<string, List<string>> Dates = new();

            Dictionary<string, List<string>> dicDates = new();            

            foreach (var center in Center)
            {
                List<Date> deserializeDate = GetDeserializeDate(center, categoryCode);

                if (deserializeDate.Count == 0)
                {
                    List<string> dateList = new();
                    dateList.Add("empty");
                    Dates[center.Value] = dateList;
                    //Console.WriteLine($"{center.Value} empty(");
                }

                else
                {
                    List<string> dateList = new();

                    foreach (var date in deserializeDate)
                    {
                        dateList.Add(date.bookingDate);
                    }
                    Dates[center.Value] = dateList;
                }

                dicDates = Dates;
            }

            return dicDates;
        }
    }
}

