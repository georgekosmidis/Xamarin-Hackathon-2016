using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.Data.Json;

namespace XDevMessaging.Notifications
{
    public class CrossPushNotificationSender
    {
        public async static void PushNotification(string url, string t1, string t2)
        {
            var toastMessage = @"<toast launch=""app-defined-string"">
                                    <visual>
                                        <binding template=""ToastGeneric"">
                                            <text> "+t1+@" </text>                 
                                        </binding>                 
                                    </visual>
                                </toast> ";

            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Post,
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            //request.Headers.Add("Content-type", "; charset=utf-8");
            request.Headers.Add("X-WindowsPhone-Target", "toast");
            request.Headers.Add("X-NotificationClass", "2");
            request.Headers.Add("'Content-Length", toastMessage.Length.ToString());

            var task = client.SendAsync(request).ContinueWith((taskwithmsg) =>
                                                                                {
                                                                                    var response = taskwithmsg.Result;

                                                                                    var jsonTask = response.Content.ReadAsStringAsync();
                                                                                    jsonTask.Wait();
                                                                                    var jsonObject = jsonTask.Result;
                                                                               });
            task.Wait();



        }
    }

}
