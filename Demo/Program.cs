using System;
using System.Collections;
using NetDuinoUtils.Utils;
using NWebREST.Web;
using Playground;

namespace Demo
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            LcdWriter.Instance.Write("Web Demo Ready! " + DateTime.Now);

            WebServerWrapper.InitializeWebEndPoints(new ArrayList
                                                        {
                                                            new BasicPage()
                                                        });

            WebServerWrapper.StartWebServer();

            RunUtil.KeepRunning();
        }
    }
}
