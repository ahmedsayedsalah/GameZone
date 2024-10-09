using GameZone.Models;

namespace GameZone.Services
{
    public interface IGamesServices
    {
        IList<Game> GetAll();
        Game? GetById(int id);
        Task CreateGame(CreateGameFormViewModel gameVM);
        Task<Game?> Update(EditFormViewModel gameVM);
        bool Delete(int id);
    }
}
