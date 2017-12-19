using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestGoogleApi
{
    public partial class refresh_token : System.Web.UI.Page
    {

        /// <summary>
        /// App 憑證資料的檔案夾
        /// </summary>
        public static string Gfolder = AppDomain.CurrentDomain.BaseDirectory + "GoogleStorage" + System.IO.Path.DirectorySeparatorChar;

        /// <summary>
        /// 通常在就是用戶的資料庫 id 
        /// </summary>
        public static string UserId = "sample_user_id";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRefreshToken_Click(object sender, EventArgs e)
        {
            var userAccessToken = JsonConvert.DeserializeObject<GoogleTokenModel>(File.ReadAllText(Gfolder + "Google.Apis.Auth.OAuth2.Responses.TokenResponse-" + UserId));
            try
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["refresh_token"] = userAccessToken.refresh_token;
                    data["client_id"] = "1088926593294-4shj9oeum763j5fd5qnlkir6evntdocs.apps.googleusercontent.com";
                    data["client_secret"] = "53YoSqiEdipRA2bp-nbFTOpW";
                    data["grant_type"] = "refresh_token";

                    var response = wb.UploadValues("https://accounts.google.com/o/oauth2/token?", "POST", data);
                    string responseUTF8 = System.Text.Encoding.UTF8.GetString(response);

                    var changeTokenResponse = JsonConvert.DeserializeObject<RefrshTokenResponse>(responseUTF8);

                    Response.Write("Token 從原本的 :" + userAccessToken.access_token + " 換成: " + changeTokenResponse.access_token);

                    userAccessToken.access_token = changeTokenResponse.access_token;
                    File.WriteAllText(Gfolder + "Google.Apis.Auth.OAuth2.Responses.TokenResponse-" + UserId, JsonConvert.SerializeObject(userAccessToken));



                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

            }
        }
    }
}