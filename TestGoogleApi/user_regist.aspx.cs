using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Calendar.v3;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestGoogleApi
{
    public partial class user_regist : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                GoogleClientSecrets clientSecret;

                using (var stream = new FileStream(Gfolder + @"\client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    clientSecret = GoogleClientSecrets.Load(stream);
                }

                IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(
                                                new GoogleAuthorizationCodeFlow.Initializer
                                                {
                                                    ClientSecrets = clientSecret.Secrets,
                                                    DataStore = new FileDataStore(Gfolder),
                                                    Scopes = new[] { CalendarService.Scope.Calendar }
                                                });

                var uri =HttpContext.Current.Request.Url.ToString();
                var code =HttpContext.Current.Request["code"];
                if (code != null)
                {
                    var token = flow.ExchangeCodeForTokenAsync(UserId, code,
                                   uri.Substring(0, uri.IndexOf("?")), CancellationToken.None).Result;
                    
                    //儲存使用者的Token
                    var oauthState = AuthWebUtility.ExtracRedirectFromState(
                        flow.DataStore, UserId, Request["state"]).Result;

                    //印出儲存狀態
                    Response.Write(oauthState);
                }

            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {

            GoogleClientSecrets clientSecret;
            //匯入專案憑證
            using (var stream = new FileStream(Gfolder + @"\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                clientSecret = GoogleClientSecrets.Load(stream);
            }

            //建立流程權限
            IAuthorizationCodeFlow flow = new GoogleAuthorizationCodeFlow(
            new GoogleAuthorizationCodeFlow.Initializer
            {

                ClientSecrets = clientSecret.Secrets,
                DataStore = new FileDataStore(Gfolder),
                Scopes = new[] { CalendarService.Scope.Calendar }
            });

            
            var uri = System.Web.HttpContext.Current.Request.Url.ToString();
            var result = new AuthorizationCodeWebApp(flow, uri, "success").AuthorizeAsync(UserId, CancellationToken.None).Result;
            if (result.RedirectUri != null)
            {
                //導去授權頁
                Response.Redirect(result.RedirectUri);
            }

        }
    }
}