using System.Collections;

namespace NWebREST.Web
{
    public interface IWebProgram
    {
        void Initialize();
        ArrayList AvailableEndPoints();
    }
}
