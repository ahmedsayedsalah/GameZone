
namespace GameZone.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext context;

        public CategoriesService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<SelectListItem> GetSelectList()
        {
           return context.Categories
                .Select(x=> new SelectListItem(){ Value= x.Id.ToString(),Text=x.Name})
                .OrderBy(x=> x.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
