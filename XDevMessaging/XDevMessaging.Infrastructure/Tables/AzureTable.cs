using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.Tables
{
    public abstract class AzureTable : IEntity
    {

        private string id;
        private DateTime createdAt;
        private DateTime updatedAt;
        private string version;
        private bool deleted;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [Microsoft.WindowsAzure.MobileServices.CreatedAt]
        public DateTime CreatedAt
        {
            get { return createdAt; }
            set { createdAt = value; }
        }

        [Microsoft.WindowsAzure.MobileServices.UpdatedAt]
        public DateTime UpdatedAt
        {
            get { return updatedAt; }
            set { updatedAt = value; }
        }
        
        //[Microsoft.WindowsAzure.MobileServices.Version]
        //public String Version
        //{
        //    get { return version; }
        //    set { version = value; }
        //}

        [Microsoft.WindowsAzure.MobileServices.Deleted]
        public bool Deleted
        {
            get { return deleted; }
            set { deleted = value; }
        }
    }
}
