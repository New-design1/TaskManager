using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal class TaskManager
    {
        WeekDays task;
        Dictionary<string, string?> dayTaskPairs;

        public TaskManager()
        {
            Initialize();
        }
        void Initialize()
        {
            string fileName = "dataFile.json";
            string jsonString = File.ReadAllText(fileName);
            task = JSONHandler.Deserialize(jsonString);

            dayTaskPairs = new Dictionary<string, string?>()
            {
                {"Понедельник", task.Monday},
                {"Вторник", task.Tuesday},
                {"Среда", task.Wednesday},
                {"Четверг", task.Thursday},
                {"Пятница", task.Friday},
                {"Суббота", task.Saturday},
                {"Воскресенье", task.Sunday}
            };
        }
        public void OutputCommands()
        {
            Console.WriteLine("\nСписок команд:" +
                "\n Обзор задач на неделю - w" +
                "\n Просмотр полной задачи в выбранный день - d" +
                "\n Новая задача - n" +
                "\n Вывести список команд - h" +
                "\n Очистить консоль - c" +
                "\n Выйти - Esc");
        }
        public void ChooseAction()
        {
            Console.WriteLine("\nВведите команду");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.W:
                    OutputWeeklySchedule();
                    break;
                case ConsoleKey.D:
                    OutputDayTask();
                    break;
                case ConsoleKey.N:
                    CreateNewTask();
                    break;
                case ConsoleKey.H:
                    OutputCommands();
                    break;
                case ConsoleKey.C:
                    Clear();
                    break;
                case ConsoleKey.Escape:
                    SaveAndExit();
                    break;
                default:
                    Console.WriteLine("\nНеизвестная команда");
                    break;
            }
        }
        public void OutputWeeklySchedule()
        {
            Console.WriteLine();
            foreach (var dayTaskPair in dayTaskPairs)
            {
                if (dayTaskPair.Key == "Суббота" || dayTaskPair.Key == "Воскресенье")
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                if (dayTaskPair.Value != null && dayTaskPair.Value.Length > 30)
                {
                    string taskPreview = dayTaskPair.Value.Substring(0, 30);
                    Console.WriteLine($"{dayTaskPair.Key} : {taskPreview}...");
                }
                else
                {
                    Console.WriteLine($"{dayTaskPair.Key} : {dayTaskPair.Value}");
                }
                Console.WriteLine("--------------------------------------------------------------");
            };

            Console.ForegroundColor = ConsoleColor.White;
        }
        public void OutputDayTask()
        {
            string day = "temporary value";

            while (!dayTaskPairs.ContainsKey(day))
            {
                Console.Write("\nДень недели: ");
                day = Console.ReadLine();
                if (!dayTaskPairs.ContainsKey(day))
                    Console.WriteLine("Неверный ввод дня недели. Список дней: Понедельник, Вторник, Среда, Четверг, Пятница, Суббота, Воскресенье");
            }

            if (day == "Суббота" || day == "Воскресенье")
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.Write($"Задачи: {dayTaskPairs[day]}\n");

            Console.ForegroundColor = ConsoleColor.White;
        }
        public void CreateNewTask()
        {
            string day = "temporary value";
            string task;

            while (!dayTaskPairs.ContainsKey(day))
            {
                Console.Write("\nДень недели: ");
                day = Console.ReadLine();
                if (!dayTaskPairs.ContainsKey(day))
                    Console.WriteLine("Неверный ввод дня недели. Список дней: Понедельник, Вторник, Среда, Четверг, Пятница, Суббота, Воскресенье");
            }

            Console.Write($"Задачи: ");
            
            task = Console.ReadLine();
            dayTaskPairs[day] = task;
            Console.WriteLine("Задача успешно сохранена");
        }
        public void Clear()
        {
            Console.Clear();
        }
        public void SaveAndExit()
        {
            Save();
            Environment.Exit(0);
        }
        public void Save()
        {
            foreach(var dayTaskPair in dayTaskPairs) 
            {
                switch (dayTaskPair.Key)
                {
                    case "Понедельник":
                        task.Monday = dayTaskPair.Value;
                        break;
                    case "Вторник":
                        task.Tuesday = dayTaskPair.Value;
                        break;
                    case "Среда":
                        task.Wednesday = dayTaskPair.Value;
                        break;
                    case "Четверг":
                        task.Thursday = dayTaskPair.Value;
                        break;
                    case "Пятница":
                        task.Friday = dayTaskPair.Value;
                        break;
                    case "Суббота":
                        task.Saturday = dayTaskPair.Value;
                        break;
                    case "Воскресенье":
                        task.Sunday = dayTaskPair.Value;
                        break;
                    default:
                        break;

                }
            }
            
            JSONHandler.Serialize(task);
        }
    }
}
