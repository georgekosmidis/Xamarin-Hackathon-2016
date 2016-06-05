using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq.Expressions;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure.Repositories
{

    public class AzureRepository : IRepository, IDisposable
    {

        MobileServiceClient client;

        public AzureRepository(string applicationURL)
        {
            this.client = new MobileServiceClient(applicationURL);
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        public async Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate)
        {
            //try
            //{
                return await client.GetTable<T>()
                 .Where(predicate)
                 .ToEnumerableAsync();
            //}
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(@"Sync error: {0}", e.Message);
            //}
            //return null;
        }

        public async Task InsertAsync<T>(T item)
        {
            await client.GetTable<T>().InsertAsync(item);
        }

        public async Task UpdateAsync<T>(T item)
        {
            await client.GetTable<T>().UpdateAsync(item);
        }

        public async Task DeleteAsync<T>(T item)
        {
            await client.GetTable<T>().DeleteAsync(item);
        }

        public void Dispose()
        {
            if (client != null)
            {
                client.Dispose();
            }
        }
    }

}
