using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationMap = new Dictionary<string, string>();
            string workingDir = Directory.GetCurrentDirectory();
            string configFile = "C:\\Users\\Admin\\Documents\\Visual Studio 2015\\Projects\\ConsoleApplication1\\ConsoleApplication1\\bin\\Debug\\logon.cfg";
            if (File.Exists(configFile))
            {
                string[] filecontent = File.ReadAllLines(configFile);
                foreach (string line in filecontent)
                {
                    char delimiter = '=';
                    string[] lineValues = line.Split(delimiter);
                    configurationMap.Add(lineValues[0], lineValues[1]);
                }
            }
            else
            {
                MessageBox.Show("Configuration file does not exist. Exiting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Initiating Logger
            Logger Log = new Logger(configurationMap["logFile"], configurationMap["errorLog"]);
            Log.WriteLog("Logging beggins");
            Log.WriteError("Error logging beggins");

            Log.Destructor(); //Finish logging and flush everything
        }
    }
}
