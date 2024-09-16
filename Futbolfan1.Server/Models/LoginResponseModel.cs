

namespace FutbolFan1.Models
{
    public class LoginResponseModel
    {
        public required string AccessToken { get; set; }
        public required UserLoginModel User { get; set; }
    }
}
