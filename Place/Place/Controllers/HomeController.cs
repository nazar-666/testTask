using System.Web.Mvc;
using Place.Services.Services.Factory;
using Place.Web.Controllers.Abstract;

namespace Place.Web.Controllers
{
    public class HomeController : GeneralController
    {
        public HomeController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}