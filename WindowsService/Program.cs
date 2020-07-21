using System;
using System.ServiceProcess;

namespace WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service = new Service())
            {
                ServiceBase.Run(service);
                //service.OnDebug();
            }
        }
    }
}
