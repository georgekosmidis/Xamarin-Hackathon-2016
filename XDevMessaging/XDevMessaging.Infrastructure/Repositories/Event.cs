using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.Repositories
{
    public class Event
    {        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }       

        [JsonProperty(PropertyName = "event_url")]
        public string EventUrl { get; set; }

        public Venue Venue { get; set; }
    }
}
