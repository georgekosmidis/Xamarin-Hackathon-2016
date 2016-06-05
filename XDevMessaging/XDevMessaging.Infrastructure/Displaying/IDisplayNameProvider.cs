using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDevMessaging.Infrastructure.Displaying
{
    public interface IDisplayNameProvider<T>
    {
        string GetDisplayName(T obj);
    }
}
