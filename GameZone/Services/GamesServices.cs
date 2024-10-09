

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string _imagePath;

        public GamesServices(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            _imagePath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public async Task CreateGame(CreateGameFormViewModel gameVM)
        {
            var coverName = await SaveCover(gameVM.Cover);

            context.Games.Add(new Game()
            {
                Name= gameVM.Name,
                Description=gameVM.Description,
                CategoryId=gameVM.CategoryId,
                Cover= coverName,
                Devices= gameVM.SelectedDevices.Select(d=> new GameDevice() { DeviceId = d }).ToList()
            });

            context.SaveChanges();
        }

        public IList<Game> GetAll()
        {
            return context.Games
                .Include(x=> x.Category)
                .Include(x=> x.Devices)
                .ThenInclude(x=> x.Device)
                .AsNoTracking()
                .ToList();
        }

        public Game? GetById(int id)
        {
            return context.Games
                .Include(x => x.Category)
                .Include(x => x.Devices)
                .ThenInclude(x => x.Device)
                .AsNoTracking()
                .SingleOrDefault(x=> x.Id==id);
        }

        public async Task<Game?> Update(EditFormViewModel gameVM)
        {
            var game = context.Games
                .Include(x => x.Devices)
                .SingleOrDefault(x => x.Id == gameVM.Id);

            if (game is null)
                return null;

            var hasNewCover = gameVM.Cover is not null;
            var oldCover = game.Cover;


            // map viewmodel to model
            game.Name = gameVM.Name;
            game.Description = gameVM.Description;
            game.CategoryId = gameVM.CategoryId;
            game.Devices = gameVM.SelectedDevices.Select(x => new GameDevice() { DeviceId = x }).ToList();

            if(hasNewCover)
            {
                // save cover in server and db
                game.Cover = await SaveCover(gameVM.Cover!);
            }

            var effectedRows = context.SaveChanges();

            if(effectedRows>0)
            {
                if(hasNewCover)
                {
                    // remove old cover from server
                    var cover = Path.Combine(_imagePath, oldCover);
                    File.Delete(cover);
                }

                return game;
            }
            else
            {
                if (hasNewCover)
                {
                    // remove new cover from server
                    var cover = Path.Combine(_imagePath, game.Cover);
                    File.Delete(cover);
                }

                return null;
            }
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var game= context.Games.Find(id);

            if (game is null)
                return isDeleted;

            context.Remove(game);
            var effectedRows= context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted= true;
                var cover= Path.Combine(_imagePath, game.Cover);
                File.Delete(cover);
            }

            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_imagePath, coverName);

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }
    }
}
