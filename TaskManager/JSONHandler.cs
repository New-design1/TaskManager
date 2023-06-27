using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class JSONHandler
    {
        public static WeekDays Deserialize(string jsonString)
        {
            WeekDays schedule = new WeekDays();
            StringBuilder stringBuilder= new StringBuilder();
            string fieldName = null;
            string fieldValue = null;

            for(int i = 0; i < jsonString.Length; i++) 
            {
                if (jsonString[i] == '"')
                {
                    i++;

                    while (jsonString[i] != '"')
                    {
                        stringBuilder.Append(jsonString[i++]);
                    }

                    if (fieldName == null)
                    {
                        fieldName = stringBuilder.ToString();
                        stringBuilder.Clear();
                    }
                    else if (fieldName != null && fieldValue == null)
                    {
                        fieldValue = stringBuilder.ToString();
                        stringBuilder.Clear();
                    }
                    if (fieldName != null && fieldValue != null)
                    {
                        WriteTask(schedule, fieldName, fieldValue);
                        fieldName = null;
                        fieldValue = null;
                    }
                }
            }

            return schedule;
        }

        static void WriteTask(WeekDays schedule, string fieldName, string fieldValue)
        {
            switch(fieldName) 
            {
                case "Monday":
                    schedule.Monday = fieldValue; 
                    break;
                case "Tuesday":
                    schedule.Tuesday = fieldValue;
                    break;
                case "Wednesday":
                    schedule.Wednesday = fieldValue;
                    break;
                case "Thursday":
                    schedule.Thursday = fieldValue;
                    break;
                case "Friday":
                    schedule.Friday = fieldValue;
                    break;
                case "Saturday":
                    schedule.Saturday = fieldValue;
                    break;
                case "Sunday":
                    schedule.Sunday = fieldValue;
                    break;
            }
        }

        public static void Serialize(WeekDays schedule)
        {
            string tasks = $"{{\r\n    \"Monday\": \"{schedule.Monday}\",\r\n    \"Tuesday\": \"{schedule.Tuesday}\",\r\n    \"Wednesday\": \"{schedule.Wednesday}\",\r\n    \"Thursday\": \"{schedule.Thursday}\",\r\n    \"Friday\": \"{schedule.Friday}\",\r\n    \"Saturday\": \"{schedule.Saturday}\",\r\n    \"Sunday\": \"{schedule.Sunday}\"\r\n}}";

            File.WriteAllText("dataFile.json", tasks);
        }
    }
}
