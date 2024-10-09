using GameZone.Settings;

namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute: ValidationAttribute
    {
        private readonly int maxfilesize;

        public MaxFileSizeAttribute(int maxfilesize)
        {
            this.maxfilesize = maxfilesize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if(file is not null)
            {
                if (file.Length > maxfilesize)
                {
                    return new ValidationResult($"Maximum allowed size is {FileSettings.MaxFileSizeInMB} MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
