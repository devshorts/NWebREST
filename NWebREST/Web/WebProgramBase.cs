using System.Collections;

namespace NWebREST.Web
{
    public abstract class WebProgramBase : IWebProgram
    {
        protected WebProgramBase()
        {
        }

        public abstract void Initialize();

        public abstract ArrayList AvailableEndPoints();
    }
}
