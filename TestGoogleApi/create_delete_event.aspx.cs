using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Web;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace TestGoogleApi
{
  


    public partial class create_delete_event : System.Web.UI.Page
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

        /// <summary>
        ///  傳入 Google Token 物件，處理成 CalendarService 後回傳
        /// </summary>
        /// <param name="GoogleTokenModelObj"></param>
        /// <returns></returns>
        public static CalendarService GetCalendarService(GoogleTokenModel GoogleTokenModelObj)
        {
            CalendarService service = null;

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

            var result = new AuthorizationCodeWebApp(flow, "", "").AuthorizeAsync(UserId, CancellationToken.None).Result;
            service = new CalendarService(new BaseClientService.Initializer
            {
                ApplicationName = "donma-test",
                HttpClientInitializer = result.Credential
            });

            return service;
        }

        protected void btnCreateEvent_Click(object sender, EventArgs e)
        {
            Event event1 = new Event()
            {
                Summary = "當麻部落格測試",
                Start = new EventDateTime()
                {
                    DateTime = DateTime.Now,
                    TimeZone = "Asia/Taipei"
                },
                End = new EventDateTime()
                {
                    DateTime = DateTime.Now.AddMinutes(1),
                    TimeZone = "Asia/Taipei"
                },

            };
            
            var d = JsonConvert.DeserializeObject<GoogleTokenModel>(File.ReadAllText(Gfolder + "Google.Apis.Auth.OAuth2.Responses.TokenResponse-"+UserId));
            CalendarService calService = GetCalendarService(d);
            EventsResource eventResource = new EventsResource(calService);
            

            var insertEntry = eventResource.Insert(event1, "primary").Execute();
            txtEventId.Text = insertEntry.Id;
           
        }

        protected void btnDelEvent_Click(object sender, EventArgs e)
        {
            var d = JsonConvert.DeserializeObject<GoogleTokenModel>(File.ReadAllText(Gfolder + "Google.Apis.Auth.OAuth2.Responses.TokenResponse-" + UserId));
            CalendarService calService = GetCalendarService(d);
            EventsResource eventResource = new EventsResource(calService);

            eventResource.Delete("primary", txtEventId.Text).Execute();
            Response.Write("成功刪除 :"　+ txtEventId.Text);

        }

    }
}