using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace EasySave_v1
{
    class Logs
    {
        public void Logsjson()
        {
            // 
            string logDirectory = @"C:\Temp";
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()  // Write logs on console
            .WriteTo.File(@"C:\Users\Lilia\source\repos\Logs\log.json", rollingInterval: RollingInterval.Day) // Écriture des logs dans un fichier JSON avec rotation quotidienne
            .CreateLogger();
        }
    }
}
