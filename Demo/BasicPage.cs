using System;
using System.Collections;
using NetDuinoUtils.Utils;
using NWebREST.Web;

namespace Demo
{
    public class BasicPage : IEndPointProvider
    {
        #region Endpoint initialization

        public void Initialize() { }

        public ArrayList AvailableEndPoints()
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

        #endregion

        #region Endpoint Execution

        private string Echo(EndPointActionArguments misc, string[] items)
        {
            String text = "";
            if (items != null && items.Length > 0)
            {
                foreach (var item in items)
                {
                    text += item + " ";
                }
            }
            else
            {
                text = "No arguments!";
            }

            LcdWriter.Instance.Write(text);

            return "OK. Wrote out: " + (text.Length == 0 ? "n/a" : text);
        }

        #endregion
    }
}
