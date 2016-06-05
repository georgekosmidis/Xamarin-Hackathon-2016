using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.Tables
{
    [Microsoft.WindowsAzure.MobileServices.DataTable("Users")]
    public class User : AzureTable
    {
        string username;
        string password;
        string email;
        string latitude;
        string longitude;
        string codingLanguage;
        string notificationId;
        string platform;

        [JsonProperty(PropertyName = "username")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [JsonProperty(PropertyName = "latitude")]
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        [JsonProperty(PropertyName = "codingLanguage")]
        public string CodingLanguage
        {
            get { return codingLanguage; }
            set { codingLanguage = value; }
        }

        [JsonProperty(PropertyName = "notificationId")]
        public string NotificationId
        {
            get { return notificationId; }
            set { notificationId = value; }
        }

        [JsonProperty(PropertyName = "platform")]
        public string Platform
        {
            get { return platform; }
            set { platform = value; }
        }
    }
}
