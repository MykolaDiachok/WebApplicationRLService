using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RLBalance.Controllers
{
    public class HomeController : Controller
    {
        private string server = "http://srv-iis.rl.int";
        private string database = "GlobalBase";
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        private CredentialCache GetCredentialCache(string inUri)
        {
            var username = "web-service";
            var password = "{htyNsVtyzGjl,thtimC1Hfpf";
            CredentialCache cc = new CredentialCache();
            cc.Add(
                new Uri(inUri),
                "NTLM",
                new NetworkCredential(username, password, "rl"));
            return cc;
        }


        //[OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetBalance()
        {
            using (WebClient webclient = new WebClient())
            {
                string uri = server + "/" + database + "/hs/rlservice/balance";

                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = false;
                webclient.Credentials = GetCredentialCache(uri);
                webclient.Headers.Add("idclient", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["login"]);
                webclient.Headers.Add("password", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["password"]);

                var jsonstring = webclient.DownloadString(uri);

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }


    }
}