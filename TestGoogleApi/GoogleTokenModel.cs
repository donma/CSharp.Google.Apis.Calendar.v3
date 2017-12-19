using System;
namespace TestGoogleApi
{


    public class GoogleTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime IssuedUtc { get; set; }
    }

    public class RefrshTokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}