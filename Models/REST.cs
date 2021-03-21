using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Converters;

using System.Web.Script.Serialization;

namespace DesktopApp.Models
{
    static class REST
    {

        public static string GetAll(string table)
        {
            string url = "https://localhost:44373/api/" + table + "/";
            var json = "";

            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch(Exception e)
                {
                    return null;
                }
               
            }
            return json;
        }

        public static string Get_ID(string table, int ID)
        {
            string url = "https://localhost:44373/api/" + table + "/" + ID.ToString() + "";
            var json = "";

            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch(Exception e)
                {
                    return null;
                }
            }
            return json;
        }


        public static void Put_ID(string table, int ID, object data)
        {
            string url = "https://localhost:44373/api/" + table + "/" + ID.ToString() + "";

            using (var client = new HttpClient())
            {
                try
                {
                    // client.BaseAddress = new Uri(url);
                    var response = client.PutAsJsonAsync(url, data).Result;
                    //   var response = client.PutAsync(new Uri(url), new StringContent(new JavaScriptSerializer().Serialize(data), Encoding.UTF8, "application/json")).Result;
                    //  var response = client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).Result;
                }
                catch (Exception e)
                {

                    
                }
                
            }
        }


        public static HttpResponseMessage Post(string table, object data)
        {
            string url = "https://localhost:44373/api/" + table + "";
            var response = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                try
                {
                    //  client.BaseAddress = new Uri(url);
                    response = client.PostAsJsonAsync(url, data).Result;
                    // response = client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json")).Result;

                }
                catch (Exception e)
                {

                }
               
            }
            return response;
        }

        public static string JoinKontaktRadnik(int id)
        {
            string url = "https://localhost:44373/api/joinkontaktradnik/" + id.ToString() + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {

                    return null;
                }
               
            }
            return json;
        }
        public static string JoinKontaktUstanova(int id)
        {
            string url = "https://localhost:44373/api/joinkontaktustanova/" + id.ToString() + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception)
                {

                    return null;
                }
               
            }
            return json;
        }

        public static string JoinKontaktDonor(int id)
        {
            string url = "https://localhost:44373/api/joinkontaktdonor/" + id.ToString() + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception)
                {

                    return null;
                }
                
            }
            return json;
        }

        public static string JoinDonacijaDoza(int id)
        {
            string url = "https://localhost:44373/api/joindonacijadoza/" + id.ToString() + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception)
                {

                    return null;
                }
                
            }
            return json;
        }

        public static string JoinDonacijaLjekarski(int id)
        {
            string url = "https://localhost:44373/api/JoinDonacijaLjekarskiPregled/" + id.ToString() + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception)
                {

                    return null;
                }
              
            }
            return json;
        }

        public static string JoinDonacijaDonor(int id)
        {
            string url = "https://localhost:44373/api/joindonacijadonor/" + id.ToString() + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception)
                {

                    return null;
                }
               
            }
            return json;
        }

        public static string EmailCheck(string email)
        {
            string url = "https://localhost:44373/api/emailcheck/" + email + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }
                
            }
            return json;
        }

        public static string GoodDoses()
        {
            string url = "https://localhost:44373/api/gooddoses";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }

        public static string GetMagacin(string type)
        {
            string url = "https://localhost:44373/api/getmagacin/"+type+"";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }


        public static string GetInstitution(int id)
        {
            string url = "https://localhost:44373/api/getinstitution/" + id + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }

        public static string GetIsporuka(int id)
        {
            string url = "https://localhost:44373/api/getisporuka/" + id + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }



        public static string GetDoza(int id)
        {
            string url = "https://localhost:44373/api/getdoza/" + id + "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }

        public static string GetdozaZaisporuku(string grupa, string tip)
        {
            string url = "https://localhost:44373/api/getdozazaisporuku/" + grupa + "/"+tip+"";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }

        public static string GetZahtjevInstitucija(int id)
        {
            string url = "https://localhost:44373/api/getzahtjevinstitucija/" + id+ "";
            var json = "";
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;
                    json = wc.DownloadString(url);
                }
                catch (Exception e)
                {
                    return null;
                }

            }
            return json;
        }

    }
}
