using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;

namespace DrivingLicensePracticalExam
{
    public class DateUpdate
    {
        SentMsg print = new();
        AllData data = new();
        Notifier notifier = new();  
        public Dictionary<string, List<string>> UpdateDate(Dictionary<string, List<string>> CityDatesA, Dictionary<string, List<string>> CityDatesB)
        {
            Dictionary<string, List<string>> update = new Dictionary<string, List<string>>();

            foreach (var cityDate in CityDatesB) 
            {
                string city = cityDate.Key;
                if (CityDatesA.ContainsKey(city))
                {
                    var listA = CityDatesA[city];
                    var listB = CityDatesB[city];

                    if (Enumerable.SequenceEqual(listA, listB))
                    {
                        Console.WriteLine($"{city} odinakovay");
                        continue;
                    }

                    else
                    {
                        List<string> listPostTg = listB.Except(listA).ToList();
                        Console.WriteLine("изменилось");
                        update[city] = listPostTg;
                    }

                }
            }
            return update;
            
        }

        public void UpdateMechanic(Dictionary<string, List<string>> cityDatesA)
        {
            int count = 0;
            while (true)
            {
                Console.WriteLine("sleep MECH");
                Thread.Sleep(TimeSpan.FromMinutes(61));
                //Thread.Sleep(TimeSpan.FromSeconds(20));
                Console.WriteLine("start mechanic");
                count ++;

                Dictionary<string, List<string>> cityDatesB = data.GetAllData("3");
                Dictionary<string, List<string>> updDates = UpdateDate(cityDatesA, cityDatesB);

                foreach (var u in updDates)
                {
                    notifier.Notify("MECHANIC UPDATE", false);
                    print.SentUpdate(u.Key, u.Value);
                }

                cityDatesA = cityDatesB;

                Console.WriteLine(count);
                if (count == 6)
                {
                    print.SentFirstMsg(cityDatesA, "3");
                    count = 0;
                }
            }
        }

        public void UpdateAutomatic(Dictionary<string, List<string>> cityDatesA)
        {         
            int count = 0;  
            while (true)
            {
                Console.WriteLine("sleep AUTH");
                Thread.Sleep(TimeSpan.FromHours(1));
                //Thread.Sleep(TimeSpan.FromSeconds(15));
                Console.WriteLine("start automatic");
                count++;

                Dictionary<string, List<string>> cityDatesB = data.GetAllData("4");
                Dictionary<string, List<string>> updDates = UpdateDate(cityDatesA, cityDatesB);
                
                foreach (var u in updDates)
                {
                    notifier.Notify("AUTHOMATIC UPDATE", false);
                    print.SentUpdate(u.Key, u.Value);
                }

                cityDatesA = cityDatesB;

                Console.WriteLine(count);
                if (count == 6)
                {
                    print.SentFirstMsg(cityDatesA, "4");
                    count = 0;
                }

            }
        }
    }
}
