using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure.Displaying
{

    public class UserDisplayNameProvider : IDisplayNameProvider<User>
    {
        public string GetDisplayName(User obj)
        {
            return string.Format("{0} ({1})", obj.Username, obj.CodingLanguage);
        }
    }
}
