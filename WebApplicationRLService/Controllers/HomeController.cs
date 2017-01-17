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
            //    webclient.Headers.Add("pasword", @"F7/83?b5bf");

            //    var jsonstring = webclient.DownloadString("http://localhost/GlobalBase/hs/rlservice/GetItemsListsAll");
            //    JArray jsonarray = JArray.Parse(jsonstring);
            //    items = jsonarray.ToObject<IList<Item>>();
            //    //Cache.Insert("GetItemsListsAll", jsonstring);

            //}

            return View();
        }

        [OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetItemsBarCode(string Barcode)
        {
            using (WebClient webclient = new WebClient())
            {
                //var username = "user";
                //var password = "password";
                //NetworkCredential creds = new NetworkCredential(username, password, "rl");


                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = true;
                //webclient.Credentials = creds;
                webclient.Headers.Add("idclient", "1");
                webclient.Headers.Add("pasword", @"F7/83?b5bf");
                webclient.Headers.Add("barcode", Barcode);

                var jsonstring = webclient.DownloadString("http://localhost/GlobalBase/hs/rlservice/GetItemsListsAll");

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }

        [OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetItems()
        {
            using (WebClient webclient = new WebClient())
            {
                //var username = "user";
                //var password = "password";
                //NetworkCredential creds = new NetworkCredential(username, password, "rl");


                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = true;
                //webclient.Credentials = creds;
                webclient.Headers.Add("idclient", "1");
                webclient.Headers.Add("pasword", @"F7/83?b5bf");

                var jsonstring = webclient.DownloadString("http://localhost/GlobalBase/hs/rlservice/GetItemsListsAll");

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }

        [OutputCache(CacheProfile = "CacheProfile")]
        public ContentResult GetManager()
        {
            using (WebClient webclient = new WebClient())
            {
                //var username = "user";
                //var password = "password";
                //NetworkCredential creds = new NetworkCredential(username, password, "rl");


                webclient.Encoding = Encoding.UTF8;
                webclient.UseDefaultCredentials = true;
                //webclient.Credentials = creds;
                webclient.Headers.Add("idclient", "1");
                webclient.Headers.Add("pasword", @"F7/83?b5bf");

                var jsonstring = webclient.DownloadString("http://localhost/GlobalBase/hs/rlservice/GetManager");

                //Cache.Insert("GetItemsListsAll", jsonstring);
                return new ContentResult { Content = jsonstring, ContentType = "application/json" }; ;
            }
        }

    }
}