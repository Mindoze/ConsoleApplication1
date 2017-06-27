using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApplication1
{
    class Logger
    {
        private StreamWriter lf;
        private StreamWriter elf;

        public Logger(string logFile, string errorLogFile)
        {
            if (!File.Exists(logFile) || !File.Exists(errorLogFile))
            {
                //Attempt to create a file
                string path = Path.GetDirectoryName(logFile);
                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                File.Create(logFile);
                File.Create(errorLogFile);
            }
            //Console.WriteLine(logFile + errorLogFile);
            lf = new StreamWriter(logFile, true) ;
            elf = new StreamWriter(errorLogFile, true);
            lf.AutoFlush = true;
            elf.AutoFlush = true;

        }

        public async void WriteLog(string Message)
        {
            if (lf.BaseStream.CanWrite == false) { System.Windows.Forms.MessageBox.Show("Cant Write to log"); }
            await lf.WriteLineAsync("[" + DateTime.Now + "]" + " " + Message);

            //this.lf.WriteLineAsync("[{0}] {1}", DateTime.Now, Message);
        }
        public async void WriteError(string Message)
        {
            if (elf.BaseStream.CanWrite == false) { System.Windows.Forms.MessageBox.Show("Cant Write to error log"); }
            await elf.WriteLineAsync("[" + DateTime.Now + "]" + " " + Message);
            //this.elf.WriteLineAsync("[{0}] {1}", DateTime.Now, Message);
        }
        public void Destructor()
        {
            WriteError("Error logging ends");
            WriteLog("Logging ends");
            if (this.lf.BaseStream.CanRead == true) { this.lf.Dispose(); }
            if (this.elf.BaseStream.CanRead == true) { this.elf.Dispose(); }
        }
    }
}
