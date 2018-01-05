using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Facebook;

namespace EMG
{
    public partial class Facebooksync : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAuthorization();
        }

        private void CheckAuthorization()
        {
            string app_id = "472853519550243";
            string app_secret = "cb39560c0d8f3d040ea4e5b3e2a7ded5";
            string scope = "publish_actions";

            if (Request["code"] == null)
            {
                Response.Redirect(string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, Request.Url.AbsoluteUri, scope));
            }
            else
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();

                string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                    app_id, Request.Url.AbsoluteUri, scope, Request["code"].ToString(), app_secret);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string vals = reader.ReadToEnd();

                    foreach (string token in vals.Split('&'))
                    {
                        //meh.aspx?token1=steve&token2=jake&...
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }
                }

                string access_token = tokens["access_token"];

                var client = new FacebookClient(access_token);
                  
                client.Post("/988407827879011/feed", new { message = "test" });
            }
        }
    }

    //class Program
    //{

    //    static void Main(string[] args)
    //    {
    //        string AppID = "1569729463341111";
    //        string AppSecret = "639a73cc78258cfa7bff7d23df9d3123";
    //        string UserId = "985602058159588";

    //        WebClient wc = new WebClient();
    //        //因為access_token會有過期失效問題，所以每次都重新取得access_token
    //        string result = wc.DownloadString("https://graph.facebook.com/oauth/access_token?client_id=" + AppID + "&client_secret=" + AppSecret + "&grant_type=client_credentials");
    //        string access_token = result.Split('=')[1];

    //        Facebook.FacebookClient client = new FacebookClient(access_token);
    //        client.Post(UserId + "/feed", new { message = "要發文的內容" });



    //        Console.ReadKey();
    //    }
    //}
}