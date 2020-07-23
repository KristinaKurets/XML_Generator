using Common;
using DataBase_Generator;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using static WindowsService.Watcher;

namespace WindowsService
{
    public class Service : ServiceBase
    {
        Watcher watcher;

        public Service()
        {
            ServiceName = "WindowsService";
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            watcher = new Watcher("C:\\Users\\Kris\\source\\repos\\XML_Generator\\LINQ\\bin\\Debug\\netcoreapp3.1");
            Thread loggerThread = new Thread(new ThreadStart(watcher.Start));
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            watcher.Stop();
            Thread.Sleep(1000);

        }
        public void OnDebug()
        {
            OnStart(null);
        }
    }

    
}

