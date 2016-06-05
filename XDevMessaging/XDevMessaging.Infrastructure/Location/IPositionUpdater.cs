using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure.Location
{
    public interface IPositionUpdater
    {
        Task UpdateUserPositionAsync(User user, Position position);
    }
}
