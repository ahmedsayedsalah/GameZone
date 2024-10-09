
namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IDevicesServicecs devicesServicecs;
        private readonly IGamesServices gamesServices;

        public GamesController(ICategoriesService categoriesService,IDevicesServicecs devicesServicecs,IGamesServices gamesServices)
        {
            this.categoriesService = categoriesService;
            this.devicesServicecs = devicesServicecs;
            this.gamesServices = gamesServices;
        }
        public IActionResult Index()
        {
            return View(gamesServices.GetAll());
        }
        public IActionResult Create()
        {
            var gameVM = new CreateGameFormViewModel()
            {
                Categories = categoriesService.GetSelectList(),
                Devices = devicesServicecs.GetSelectList()
            };
            return View(gameVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel gameVM)
        {
            if(!ModelState.IsValid)
            {
                gameVM.Categories = categoriesService.GetSelectList();
                gameVM.Devices = devicesServicecs.GetSelectList();

                return View(gameVM);
            }

            //save in db
            await gamesServices.CreateGame(gameVM);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var game= gamesServices.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);
        }

        public IActionResult Edit(int id)
        {
            var game= gamesServices.GetById(id);

            if(game is null)
                return NotFound();

            // map model to viewmodel
            var gameVM = new EditFormViewModel()
            {
                Id = id,
                Name= game.Name,
                Description= game.Description,
                CategoryId= game.CategoryId,
                SelectedDevices= game.Devices.Select(x=> x.DeviceId).ToList(),
                Categories= categoriesService.GetSelectList(),
                Devices= devicesServicecs.GetSelectList(),
                CurrentCover= game.Cover,
            };

            return View(gameVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditFormViewModel gameVM)
        {
            if (!ModelState.IsValid)
            {
                gameVM.Categories = categoriesService.GetSelectList();
                gameVM.Devices = devicesServicecs.GetSelectList();

                return View(gameVM);
            }

            var game = await gamesServices.Update(gameVM);

            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = gamesServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
