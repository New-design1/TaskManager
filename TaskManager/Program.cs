using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace TaskManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            taskManager.OutputCommands();
            
            while (true) 
                taskManager.ChooseAction();
        }
    }
}