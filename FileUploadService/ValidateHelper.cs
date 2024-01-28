using System;
namespace FileUploadService
{
	public class ValidateHelper
	{
        public static bool ValidateWithOutNullOrWhiteSpace(string input)
        {
            var validate = !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
            return validate;
        }

        public static bool ValidateWithOutNull(string input)
        {
            var validate = !string.IsNullOrEmpty(input);
            return validate;
        }

    }
}

