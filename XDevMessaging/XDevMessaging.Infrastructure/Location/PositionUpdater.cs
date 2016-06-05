using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDevMessaging.Infrastructure.Repositories;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure.Location
{
    public class PositionUpdater : IPositionUpdater
    {
        protected IRepository Repository { get; set; }

        public PositionUpdater(IRepository repository)
        {
            this.Repository = repository;
        }

        public async Task UpdateUserPositionAsync(User user, Position position)
        {
            user.Latitude = position.Latitude.ToString(CultureInfo.InvariantCulture);
            user.Longitude = position.Longitude.ToString(CultureInfo.InvariantCulture);

            await this.Repository.UpdateAsync(user);
        }

        public bool IsGpsEnabled() {
            return CrossGeolocator.Current.IsGeolocationEnabled;
        }
    }
}
