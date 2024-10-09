
namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel: GameFormViewModel
    {
        //[RegularExpression(@".+(jpg|png)", ErrorMessage = "only allowed extensions jpg,png")]
        [AllowedExtensions(FileSettings.AllowExtensions),MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; }
    }
}
