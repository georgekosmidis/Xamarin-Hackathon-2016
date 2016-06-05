using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.Tables
{
    [Microsoft.WindowsAzure.MobileServices.DataTable("Messages")]
    public class Message : AzureTable
    {
        private string from;
        private string to;
        private string body;

        [JsonProperty(PropertyName = "from")]
        public string From
        {
            get { return from; }
            set { from = value; }
        }

        [JsonProperty(PropertyName = "to")]
        public string To
        {
            get { return to; }
            set { to = value; }
        }

        [JsonProperty(PropertyName = "body")]
        public string Body
        {
            get { return body; }
            set { body = value; }
        }       
    }
}
