// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Globalization;
using TelegramBot;

namespace DrivingLicensePracticalExam
{
    public class Program
    {
        static void Main()
        {            
            SentMsg print = new();
            AllData data = new();
            DateUpdate update = new();

            Dictionary<string, List<string>> cityDatesA_Mechanic = data.GetAllData("3");
            print.SentFirstMsg(cityDatesA_Mechanic, "3");

            Dictionary<string, List<string>> cityDatesA_Automatic = data.GetAllData("4");
            print.SentFirstMsg(cityDatesA_Automatic, "4");
            
            void updAuth()
            {
                update.UpdateAutomatic(cityDatesA_Automatic);
            }
            // создаем новый поток
            Thread myThread = new Thread(updAuth);
            // запускаем поток myThread
            myThread.Start();

            update.UpdateMechanic(cityDatesA_Mechanic);  
        }

    }
}
