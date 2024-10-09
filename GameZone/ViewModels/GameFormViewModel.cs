
namespace GameZone.ViewModels
{
    public class GameFormViewModel
    {
        [MaxLength(250)]
        public string Name { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IList<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        [Display(Name = "Supported Devices")]
        public IList<int> SelectedDevices { get; set; }
        public IList<SelectListItem> Devices { get; set; } = new List<SelectListItem>();

        [MaxLength(2500)]
        public string Description { get; set; }
    }
}
