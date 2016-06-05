using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure.Extensions
{
    public static class PinExtensions
    {
        private static Dictionary<int, User> PinsUsers = new Dictionary<int, User>();

        public static void SetUser(this Pin pin, User user)
        {
            if (PinsUsers.ContainsKey(pin.GetHashCode()))
            {
                PinsUsers[pin.GetHashCode()] = user;
            }
            else
            {
                PinsUsers.Add(pin.GetHashCode(), user);
            }

        }

        public static User GetUser(this Pin pin)
        {
            return PinsUsers[pin.GetHashCode()];
        }


        public static void CleanUp()
        {
            PinsUsers = new Dictionary<int, User>();
        }
    }
}
