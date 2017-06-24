using System.Web.Mvc;
using Lx.Utilities.Contracts.Authentication.Interfaces;

namespace Pec.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOAuthClientService _oauthClientService;

        public HomeController(IOAuthClientService oauthClientService)
        {
            _oauthClientService = oauthClientService;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}