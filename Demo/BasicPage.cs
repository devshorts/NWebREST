using System;
using System.Collections;
using NetDuinoUtils.Utils;
using NWebREST.Web;

namespace Playground
{
    public class BasicPage : WebProgramBase
    {
       
        #region Endpoint initialization

        public override void Initialize()
        {
            
        }

        protected override ArrayList GetEndPoints()
        {
            var list = new ArrayList
                           {
                               new EndPoint
                                   {
                                       Action = Echo,
                                       Name = "echoArgs",
                                       Description = "Writes the URL arguments to a serial LCD hooked up to COM1"
                                   }
                           };
            return list;
        }

        private string Echo(EndPointActionArguments misc, object[] items)
        {
            String text = "";
            if (items != null && items.Length > 0)
            {
                
                foreach(var item in items)
                {
                    text += item.ToString().Replace("%20", " ") + " ";
                }

                LcdWriter.Instance.Write(text);

                if (text.Length > 16 * 2)
                {
                    return "TEXT TOO LONG, DISPLAYING: " +
                           text.Substring(text.Length - 16 * 2, 16 * 2 - (text.Length - 16 * 2));
                }
            }

            return "OK. Wrote out: " + (text.Length == 0 ? "n/a" :  text);
        }

        #endregion

    }
}
