using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XDevMessaging.ViewModels {
    public class MessageVM : INotifyPropertyChanged {

        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; RaisePropertyChanged(); }
        }

        private DateTime timeStamp;

        public DateTime TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; RaisePropertyChanged(); }
        }

        private bool isIncoming;

        public bool IsIncoming
        {
            get { return isIncoming; }
            set { isIncoming = value; RaisePropertyChanged(); }
        }

        private string backgroundColor;

        public string BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; RaisePropertyChanged(); }
        }

        private string horizontalTextAlignment;

        public string HorizontalTextAlignment
        {
            get { return horizontalTextAlignment; }
            set { horizontalTextAlignment = value; RaisePropertyChanged(); }
        }

        private ImageSource image;

        public ImageSource Image
        {
            get { return image; }
            set { image = value; RaisePropertyChanged(); }
        }

        private string column;

        public string Column
        {
            get { return column; }
            set { column = value; RaisePropertyChanged(); }
        }

        private string textColor;

        public string TextColor
        {
            get { return textColor; }
            set { textColor = value; RaisePropertyChanged(); }
        }
        
        public string From { get; set; }
        
        public string To { get; set; }
               
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged( [CallerMemberName]  string propertyName = "" ) {
            if ( PropertyChanged != null ) {
                PropertyChanged( this, new PropertyChangedEventArgs( propertyName ) );
            }
        }
    }
}
