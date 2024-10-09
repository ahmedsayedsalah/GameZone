using System.Diagnostics;


namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesServices gamesServices;

        public HomeController(IGamesServices gamesServices)
        {
            this.gamesServices = gamesServices;
        }

        public IActionResult Index()
        {

            return View(gamesServices.GetAll());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
