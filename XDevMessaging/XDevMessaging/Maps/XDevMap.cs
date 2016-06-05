using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace XDevMessaging.Maps
{
    public class XDevMap : Map
    {
        public List<XDevPin> XDevPins { get; set; }
    }
}
