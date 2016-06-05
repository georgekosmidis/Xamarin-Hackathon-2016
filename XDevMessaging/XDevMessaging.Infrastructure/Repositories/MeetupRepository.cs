using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XDevMessaging.Infrastructure.Repositories
{
    public class MeetupRepository
    {
        public async Task<IEnumerable<Event>> GetLocalGroups(string Latitute, string Longitude)
        {            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.meetup.com/2/open_events?key=205e524637192e7b427b6e307a161465&format=json&lon="+Longitude+"&limited_events=False&photo-host=public&radius=20000&category=34&lat="+Latitute+"&desc=False&status=upcoming&sign=true");
            //if(Device.OS != TargetPlatform.WinPhone)
            //{
                request.ContentType = "application/json;charset=utf-8";
            //}
            
            request.Method = "GET";            

            var response = await request.GetResponseAsync ();
            var respStream = response.GetResponseStream();
            //respStream.Flush ();

            using (StreamReader sr = new StreamReader (respStream)) {
                //Need to return this response 
                string strContent = sr.ReadToEnd ();
                respStream = null;

                //var resp = JsonConvert.DeserializeObject<Dictionary<string,string>>(strContent);
                //var result = JsonConvert.DeserializeObject<List<Event>>(resp["result"]);

                
                JObject resultObject = JObject.Parse(strContent);

                // get JSON result objects into a list
                IList<JToken> results = resultObject["results"].Children().ToList();

                // serialize JSON results into .NET objects
                IList<Event> listResults = new List<Event>();
                foreach (JToken result in results)
                {
                    Event searchResult = JsonConvert.DeserializeObject<Event>(result.ToString());
                    listResults.Add(searchResult);
                }

                return listResults;
            }
        }
    }
}
