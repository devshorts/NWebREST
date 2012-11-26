using System.Collections;

namespace NWebREST.Web
{
    public abstract class WebProgramBase : IWebProgram
    {
        protected WebProgramBase()
        {
        }

        public abstract void Initialize();

        public void Register()
        {
            // register our web api callbacks
            WebServerWrapper.RegisterEndPoints(GetEndPoints());
        }

        protected abstract ArrayList GetEndPoints();
    }
}
