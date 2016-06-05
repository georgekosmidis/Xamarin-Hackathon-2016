using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XDevMessaging.Infrastructure;
using XDevMessaging.Infrastructure.Repositories;
using XDevMessaging.Infrastructure.Tables;
using XDevMessaging.ViewModels;

namespace XDevMessaging.Pages
{

    public partial class ChatPage : ContentPage
    {

        public ObservableCollection<MessageVM> MessagesObjectCollection { get; set; }
        private AppContext AppContext { get; set; }
        private User Chatter;
        private DateTime lastTimestamp = new DateTime();
        private bool FirstItemSource = true;
        private Task MessagesUpdateTask;
        private bool IsTaskCanceled = false;


        public ChatPage(AppContext appContext, User chatter)
        {
            this.AppContext = appContext;
            this.Chatter = chatter;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await RefreshMessages();
            IsTaskCanceled = false;
            MessagesUpdateTask = new Task(async () =>
            {
                while (!IsTaskCanceled)
                {
                    await GetMessagesAsync(true);
                    await Task.Delay(2000);
                }
            });
            MessagesUpdateTask.Start();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            IsTaskCanceled = true;
        }

        private async Task RefreshMessages()
        {
            await GetMessagesAsync(false);

        }

        private async void OnRefresh(object sender, EventArgs args)
        {
            await GetMessagesAsync(false);
        }

        private async Task GetMessagesAsync(bool fromTask)
        {
            IEnumerable<Message> messages;
            if (lastTimestamp == DateTime.MinValue)
                messages = await AppContext.Repository.GetAsync<Message>(x => (x.From == AppContext.CurrentUser.Id && x.To == Chatter.Id) ||
                                                                              (x.From == Chatter.Id && x.To == AppContext.CurrentUser.Id));
            else
                messages = await AppContext.Repository.GetAsync<Message>(x => ((x.From == AppContext.CurrentUser.Id && x.To == Chatter.Id) ||
                                                                              (x.From == Chatter.Id && x.To == AppContext.CurrentUser.Id)) && x.CreatedAt > lastTimestamp);

            if (messages.Count() > 0)
            {
                lastTimestamp = messages.OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt;

                IEnumerable<MessageVM> list = messages.OrderByDescending(x => x.CreatedAt).Select(x => new MessageVM()
                {
                    From = Chatter.Id == x.From ? Chatter.Username : AppContext.CurrentUser.Username,
                    To = Chatter.Id == x.To ? Chatter.Username : AppContext.CurrentUser.Username,
                    HorizontalTextAlignment = (x.From == AppContext.CurrentUser.Id ? "Start" : "End"),
                    TimeStamp = x.CreatedAt,
                    IsIncoming = x.From == AppContext.CurrentUser.Id,
                    TextColor = (x.From == AppContext.CurrentUser.Id ? "White" : "Black"),
                    BackgroundColor = (x.From == AppContext.CurrentUser.Id ? "#03A9F4" : "#F5F5F5"),
                    //Align = (x.From == AppContext.CurrentUser.Id ? "Start" : "End"),
                    Body = x.Body
                });

                if (FirstItemSource)
                {
                    FirstItemSource = false;
                    //if (fromTask)
                    //{
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        MessagesObjectCollection = new ObservableCollection<MessageVM>(list);
                        messageList.ItemsSource = MessagesObjectCollection;
                    });   //}

                }
                else
                {
                    foreach (var msg in list)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            MessagesObjectCollection.Add(msg);
                        });
                    }
                }

               
            }
        }

        private async void SendMessageCommand(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(InputText.Text))
                return;
            string text = InputText.Text;
            InputText.Text = string.Empty;
            Message message = new Message { Body = text, From = AppContext.CurrentUser.Id, To = Chatter.Id };
            await AppContext.Repository.InsertAsync<Message>(message);
            await RefreshMessages();
        }
    }
}

