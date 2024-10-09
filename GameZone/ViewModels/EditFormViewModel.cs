

namespace GameZone.ViewModels
{
    public class EditFormViewModel: GameFormViewModel
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowExtensions), MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; }
    }
}
