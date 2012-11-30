using System;
using System.Collections;
using Microsoft.SPOT.Hardware;
using NetDuinoUtils.Utils;
using NWebREST.Web;
using SecretLabs.NETMF.Hardware.NetduinoPlus;

namespace Demo
{
    public class ButtonWeb : IEndPointProvider
    {
        #region Data

        private static OutputPort Led;

        private static Boolean _buttonPushed;

        private static readonly object LockObject = new object();

        private static bool _previousButtonStatus = false;

        #endregion

        public ButtonWeb()
        {
            Led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }
        #region Web Server Endpoint Implementation

        private static string ButtonStatus(EndPointActionArguments misc, object[] items)
        {
            lock (LockObject)
            {
               return "Button status is " + _buttonPushed;
            }
        }

        #endregion

        #region Endpoint initialization

        public void Initialize()
        {
            // set our pin listeners on the button
            ThreadUtil.Start(() => ButtonUtils.OnBoardButtonPushed(WriteToLed));
        }

        public ArrayList AvailableEndPoints()
        {
            var list = new ArrayList
                           {
                               new EndPoint
                                   {
                                       Action = ButtonStatus,
                                       Name = "ButtonStatus",
                                       Description = "Outputs the button status of the on-board push button. "
                                   }
                           };
            return list;

        }

        #endregion

        #region Button Handlers

        
        private static void WriteToLed(bool buttonpushed)
        {
            lock (LockObject)
            {
                _buttonPushed = buttonpushed;
            }

            if (_previousButtonStatus != buttonpushed)
            {
                _previousButtonStatus = buttonpushed;
                LcdWriter.Instance.Write("Button " + (buttonpushed ? "pushed" : "released"));
            }

            Led.Write(buttonpushed);

        }

        #endregion
    }
}
