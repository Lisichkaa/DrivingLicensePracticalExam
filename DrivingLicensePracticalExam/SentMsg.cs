using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot;

namespace DrivingLicensePracticalExam
{
    public class SentMsg
    {
        Notifier notifier = new();

        public void SentFirstMsg(Dictionary<string, List<string>> dicDates, string categoryCode)
        {      
            string print = "";
            if (categoryCode == "3") 
            {
                print = "MECHANIC";
            }
            else if (categoryCode == "4")
            {
                print = "AUTOMATIC";
            }


            foreach (var city in dicDates)
            {
                print += "\n" + city.Key + "\n";

                foreach (var date in dicDates[city.Key])
                {
                    print += date + "\n";
                }
            }
            notifier.Notify(print);

            Console.WriteLine("sent first msg"); 
        }

        public void SentUpdate(string city, List<string> postTg)
        {
            string print = "";
            print += city + "\n";
            foreach (var post in postTg)
            {
                print += post + "\n";
            }
            notifier.Notify(print);
            Console.WriteLine("sent update");

        }

        public void SentUpdateNew(Dictionary<string, List<string>> updateDates, string categoryCode)
        {
            string print = "";
            if (categoryCode == "3")
            {
                print = "MECHANIC UPDATE" + "\n";
            }
            if (categoryCode == "4")
            {
                print = "AUTHOMATIC UPDATE" + "\n";
            }

            foreach (var city in updateDates.Keys)
            {
                print += "\n" + city + "\n";

                foreach (var dates in updateDates[city])
                {
                    print += dates + "\n";
                }
            }

            notifier.Notify(print);
            Console.WriteLine("sent update");

        }
    }
}
