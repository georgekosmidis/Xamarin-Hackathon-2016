using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.Repositories
{
    public class Venue
    {
        [JsonProperty(PropertyName ="address_1" )]
        public string Address { get; set; }

        [JsonProperty(PropertyName ="name" )]
        public string Name { get; set; }

        [JsonProperty(PropertyName ="lon" )]
        public string Lon { get; set; }

        [JsonProperty(PropertyName ="lat" )]
        public string Lat { get; set; }

    }
}
