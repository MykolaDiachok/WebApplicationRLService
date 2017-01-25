using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.Web.Caching;
using WebApplicationRLService.Models;

namespace WebApplicationRLService.Controllers
{
    public class HomeController : Controller
    {
        private string server = "http://srv-iis.rl.int";
        private string database = "GlobalBase1";

        private CredentialCache GetCredentialCache(string inUri)
        {
            var username = "web-service";
            var password = "{htyNsVtyzGjl,thtimC1Hfpf";
             CredentialCache cc =  new CredentialCache();
            cc.Add(
                new Uri(inUri),
                "NTLM",
                new NetworkCredential(username, password, "rl"));
            return cc;
        }

        // GET: Home
        // [OutputCache(CacheProfile = "CacheProfile")]
        public ActionResult Index()
        {
            //IList<Item> items = new List<Item>();
            //using (WebClient webclient = new WebClient())
            //{
            //    var username = "user";
            //    var password = "password";
            //    NetworkCredential creds = new NetworkCredential(username, password, "rl");


            //    webclient.Encoding = Encoding.UTF8;
            //    webclient.UseDefaultCredentials = true;
            //    webclient.Credentials = creds;
            //    webclient.Headers.Add("idclient", "1");
            //    webclient.Headers.Add("password", @"F7/83?b5bf");

            //    var jsonstring = webclient.DownloadString("http://localhost/GlobalBase/hs/rlservice/GetItemsListsAll");
            //    JArray jsonarray = JArray.Parse(jsonstring);
            //    items = jsonarray.ToObject<IList<Item>>();
            //    //Cache.Insert("GetItemsListsAll", jsonstring);

            //}

            return View();
        }


        //[OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetItemsSerial()
        {            
            using (WebClient webclient = new WebClient())
            {
                string uri = server + "/" + database + "/hs/rlservice/SearchSearialNumber";
                
                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = false;
                webclient.Credentials = GetCredentialCache(uri);
                webclient.Headers.Add("idclient", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["login"]);
                webclient.Headers.Add("password", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["password"]);
                webclient.Headers.Add("serial", Request.Form["serial"]);

                var jsonstring = webclient.DownloadString(uri);

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }


        //[OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetItemsBarCode()
        {

            using (WebClient webclient = new WebClient())
            {
                string uri = server + "/" + database + "/hs/rlservice/SearchBarCode";                         

                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = false;
                webclient.Credentials = GetCredentialCache(uri);
                webclient.Headers.Add("idclient", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["login"]);
                webclient.Headers.Add("password", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["password"]);
                webclient.Headers.Add("barcode", Request.Form["barcode"]);

                var jsonstring = webclient.DownloadString(uri);

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }


        [OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetItems()
        {
            using (WebClient webclient = new WebClient())
            {
                var uri = server + "/" + database + "/hs/rlservice/GetItemsListsAll";
               
                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = true;
                webclient.Credentials = GetCredentialCache(uri);
                webclient.Headers.Add("idclient", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["login"]);
                webclient.Headers.Add("password", HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["password"]);

                var jsonstring = webclient.DownloadString(uri);

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }

        [OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetManager()
        {

            using (WebClient webclient = new WebClient())
            {
                
                var uri = server + "/" + database + "/hs/rlservice/GetManager";

                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = true;
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