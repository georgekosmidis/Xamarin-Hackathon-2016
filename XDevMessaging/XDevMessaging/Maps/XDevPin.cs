using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace XDevMessaging.Maps
{
    public class XDevPin
    {
        public XDevPin()
        {
            pin = new Pin();
        }
        private Pin pin;
        public Pin Pin { get { return pin; } private set { pin = value; } }
        public string Image { get; set; }
    }
}
