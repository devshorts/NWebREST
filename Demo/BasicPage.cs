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

        private string Echo(EndPointActionArguments misc, object[] items)
        {
            String text = "";
            if (items != null && items.Length > 0)
            {
                foreach (var item in items)
                {
                    text += item.ToString() + " ";
                }

                LcdWriter.Instance.Write(text);
            }
            else
            {
                LcdWriter.Instance.Write("No arguments!");
            }

            return "OK. Wrote out: " + (text.Length == 0 ? "n/a" : text);
        }

        #endregion
    }
}
