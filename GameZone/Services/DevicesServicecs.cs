
namespace GameZone.Services
{
    public class DevicesServicecs : IDevicesServicecs
    {
        private readonly ApplicationDbContext context;

        public DevicesServicecs(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IList<SelectListItem> GetSelectList()
        {
            return context.Devices
                .Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name })
                .OrderBy(x => x.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
