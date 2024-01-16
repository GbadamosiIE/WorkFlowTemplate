using System.Text.RegularExpressions;
using Factory.Database;
using Factory.Entension;
using Factory.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Factory.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FactoryController: ControllerBase
    {
        [HttpGet("factory")]
        public IActionResult Get(string type)
        {
            var credit = FactoryCredit.CreateCredit(type);
            return Ok(credit.GetType().Name);
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(UserDb.userInfos);

        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] DynamicFormModel dynamicForm)
        {
             
             var dynamicFields = dynamicForm.Fields;
             if (!dynamicFields.ContainsKey("LastName") ||
        !dynamicFields.ContainsKey("FirstName") ||
        !dynamicFields.ContainsKey("Gender") ||
        !dynamicFields.ContainsKey("Age") ||
        !dynamicFields.ContainsKey("Email") ||
        !dynamicFields.ContainsKey("PhoneNumber") ||
        !dynamicFields.ContainsKey("SSN"))
    {
        return BadRequest("Missing required fields");
    }
    foreach (var field in dynamicFields)
    {
        string fieldName = field.Key;
        dynamic fieldValue = field.Value;

        switch (fieldName)
        {
            case "LastName":
                // Example: Validate and process Last Name
                ValidateAndProcessLastName(fieldValue.ToString());
                break;

            case "FirstName":
                // Example: Validate and process First Name
                ValidateAndProcessFirstName(fieldValue.ToString());
                break;

            case "Gender":
                // Example: Validate and process Gender
                ValidateAndProcessGender(fieldValue.ToString());
                break;

            case "Age":
                // Example: Validate and process Age
                ValidateAndProcessAge(fieldValue.ToString());
                break;

            case "Email":
                // Example: Validate and process Email
                ValidateAndProcessEmail(fieldValue.ToString());
                break;

            case "PhoneNumber":
                // Example: Validate and process Phone Number
                ValidateAndProcessPhoneNumber(fieldValue.ToString());
                break;

            case "SSN":
                // Example: Validate and process SSN
                ValidateAndProcessSSN(fieldValue.ToString());
                break;
            default:
                break;
        }
        
        }
        
            var userInformation = dynamicForm.ToUserInfo();
            UserDb.userInfos.Add(userInformation);
        return Ok($"Registration successful {UserDb.userInfos.Count()}");
        
    }

        private void ValidateAndProcessFirstName(string fieldValue)
        {
            // Example: Validate and process First Name
            //use regex to validate first name
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            if (!regex.IsMatch(fieldValue))
            {
                throw new ArgumentException("First name must be alphabetic");
            }
        }

        private void ValidateAndProcessLastName(string fieldValue)
        {
            // Example: Validate and process Last Name
            //use regex to validate last name
            Regex regex = new Regex(@"^[a-zA-Z]+$");
            if (!regex.IsMatch(fieldValue))
            {
                throw new ArgumentException("Last name must be alphabetic");
            }
        }

        private void ValidateAndProcessSSN(string fieldValue)
        {
            if (fieldValue == null)
            {
                throw new ArgumentNullException(nameof(fieldValue));
            }
            if (fieldValue.Length != 11)
            {
                throw new ArgumentException("SSN must be 11 digits");
            }
            if (fieldValue[0] == '0')
            {
                throw new ArgumentException("SSN cannot start with 0");
            }
            Regex regex = new Regex(@"^\d{3}-\d{2}-\d{4}$");
            if (!regex.IsMatch(fieldValue))
            {
                throw new ArgumentException("SSN must be in the format 123-45-6789");
            }
        }

        private void ValidateAndProcessPhoneNumber(string fieldValue)
        {
            // Example: Validate and process Phone Number
            //use regex to validate phone number and format it with country code and use for all countries 
           // Regex regex = new Regex(@"^\d{14}$");
            if (fieldValue.Length != 14)
            {
                  throw new ArgumentException("Phone number must be 14 digits");
            }
        }

        private void ValidateAndProcessEmail(string fieldValue)
        {
            // Example: Validate and process Email
            //use regex to validate email address
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.IsMatch(fieldValue))
            {
                throw new ArgumentException("Email must be in the format");
            }
        }

        private void ValidateAndProcessAge(string fieldValue)
        {
            // Example: Validate and process Age
            //use regex to validate age must be greater than 18
            //Regex regex = new Regex(@"^\d{2}$");
            var age = int.Parse(fieldValue);
            if (age < 18)
            {
                throw new ArgumentException("Candidate must be 18 years or older");
            }
        }

        private void ValidateAndProcessGender(string fieldValue)
        {
            //use regex to validate Gender
            Regex regex = new Regex(@"^([Mm]ale|[Ff]emale)$");
            if(!regex.IsMatch(fieldValue)){
                throw new ArgumentException("Invalid Gebder");
            }
        }
    [HttpPost("upload")]
    public IActionResult UploadVideo([FromForm] VideoUploadRequest request)
    {
        // Check if the request contains a file
        if (request.VideoFile == null || request.VideoFile.Length == 0)
        {
            return BadRequest("No video file provided");
        }
        //validate if video is video not other type of documets like files, images, etc
        if (request.VideoFile.ContentType != "video/mp4")
        {
            return BadRequest(new ArgumentException("Only video files are allowed"));
        }
        // Convert the video file to Base64 string
        string base64Content;
        using (var memoryStream = new MemoryStream())
        {
            request.VideoFile.CopyTo(memoryStream);
            base64Content = Convert.ToBase64String(memoryStream.ToArray());
        }
        // Save to the database
        var video = new Video
        {
            Name = request.VideoName,
            Content = base64Content
        };

        UserDb.videos.Add(video);
        return Ok(new { Message = "Video uploaded successfully" });
    }
    }
    }