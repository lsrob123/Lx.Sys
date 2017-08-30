using Lx.Utilities.Services.WindowsService.Topshelf;

namespace Lx.Communications.Endpoint
{
    internal class Program : ProgramBase<ServiceManager>
    {
        private static void Main(string[] args)
        {
            Init();
        }
    }
}