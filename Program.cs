using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace EasySave_v1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize logs
            Logs logjson = new Logs();
            logjson.Logsjson();

            try
            {
                // Launch application
                Menu script = new Menu();
                script.script();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error has occured");
            }
            finally
            {
                // Close and flush Serilog
                Log.CloseAndFlush();
            }
        }
    }
}
