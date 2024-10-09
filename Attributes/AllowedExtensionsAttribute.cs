namespace GameZone.Attributes
{
    public class AllowedExtensionsAttribute: ValidationAttribute
    {
        private readonly string allowedExtensition;

        public AllowedExtensionsAttribute(string allowedExtensition)
        {
            this.allowedExtensition = allowedExtensition;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var extension= Path.GetExtension(file.FileName);

                var isAllowed= allowedExtensition.Split(',').Contains(extension,StringComparer.OrdinalIgnoreCase);

                if (!isAllowed)
                {
                    return new ValidationResult($"Only {allowedExtensition} are allowed");
                }
            }
            return ValidationResult.Success;
        }
    }
}
