using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class MultipartFormDataAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        if (request.HasFormContentType 
            && request.ContentType.StartsWith("multipart/form-data", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }
        context.Result = new StatusCodeResult(StatusCodes.Status415UnsupportedMediaType);
    }
}
public class VideoUploadRequest
{
    public string VideoName { get; set; }
    public IFormFile VideoFile { get; set; }
}
public class Video
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
}
public class UserInfo
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("ssn")]
        public string SSN { get; set; }

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("languagePreference")]
        public string LanguagePreference { get; set; }

        [JsonProperty("themePreference")]
        public string ThemePreference { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("securityQuestion")]
        public string SecurityQuestion { get; set; }

        [JsonProperty("securityAnswer")]
        public string SecurityAnswer { get; set; }

        [JsonProperty("MiddleName")]
        public string MiddleName { get; set; }

        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }

        [JsonProperty("organizationAddress")]
        public string OrganizationAddress { get; set; }

        [JsonProperty("CEO")]
        public string CEO { get; set; }

        [JsonProperty("CEO address")]
        public string CEOAddress { get; set; }

        [JsonProperty("ceo phonenumber")]
        public string CeoPhonenumber { get; set; }

        [JsonProperty("HR office")]
        public string HROffice { get; set; }

        [JsonProperty("hr name")]
        public string HrName { get; set; }

        [JsonProperty("transportsystem")]
        public string Transportsystem { get; set; }
    }