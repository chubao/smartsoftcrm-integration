using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using SmartlifeCRMIntegration.HTTP;

namespace SmartlifeCRMIntegration
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {

            try
            {

                HostFactory.Run(x =>
                {

                    x.Service<ServiceManager>(s =>
                    {
                        s.ConstructUsing(name => new ServiceManager(name));
                        s.WhenStarted((tc, hostControl) => tc.Start(hostControl));
                        s.WhenStopped((tc, hostControl) => tc.Stop(hostControl));
                    });

                    x.RunAsLocalSystem();
                    x.StartAutomaticallyDelayed();
                    x.SetDescription(Configuration.DefaultServiceDescription);
                    x.SetDisplayName(Configuration.DefaultServiceDisplayName);
                    x.SetServiceName(Configuration.DefaultServiceName);
                });
                Console.Read();
            }
            catch (Exception ex) {
                logger.Error(ex.Message, ex);
            }
        }
    }
}
