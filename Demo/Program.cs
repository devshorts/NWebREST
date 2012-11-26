using System;
using System.Collections;
using NetDuinoUtils.Utils;
using NWebREST.Web;

namespace Demo
{
    public class Program
    {
public static void Main()
{
    LcdWriter.Instance.Write("Web Demo Ready! " + DateTime.Now.TimeOfDay);

    WebServerWrapper.InitializeWebEndPoints(new ArrayList
                                                {
                                                    new BasicPage(),
                                                    new ButtonWeb()
                                                });

    WebServerWrapper.StartWebServer();

    RunUtil.KeepRunning();
}
    }
}
