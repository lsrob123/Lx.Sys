using System.Threading.Tasks;
using System.Web.Mvc;
using Lx.Utilities.Contracts.Authentication.DTOs;
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

        public async Task<ActionResult> Login()
        {
            var formData = Request.Form;
            if (formData == null || !formData.HasKeys())
                return RedirectToAction(nameof(Index));

            var request = new GetTokensRequest
            {
                Username = formData["username"],
                Password = formData["password"]
            };
            var response = await _oauthClientService.GetTokensAsync(request);
            if (!response.IsError && !string.IsNullOrWhiteSpace(response.AccessToken) &&
                !string.IsNullOrWhiteSpace(response.RefreshToken))
                return RedirectToAction(nameof(Home));

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}