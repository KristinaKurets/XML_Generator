using System;
using System.ServiceProcess;
using WindowsService.Jobs;

namespace WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new Service())
            {
                ServiceBase.Run(service);
                Scheduler.Start();
                //service.OnDebug();
            }
           
        }
    }
}
