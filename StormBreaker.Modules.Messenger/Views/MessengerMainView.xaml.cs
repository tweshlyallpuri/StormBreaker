using Microsoft.AspNetCore.SignalR.Client;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StormBreaker.Modules.Messenger.Views
{
    /// <summary>
    /// Interaction logic for MessengerMainView.xaml
    /// </summary>
    public partial class MessengerMainView : UserControl, INavigationAware
    {
        HubConnection _connection;
        public MessengerMainView()
        {
            InitializeComponent();
            //connection = new HubConnectionBuilder()
            //   .WithUrl("http://localhost:53353/ChatHub")
            //   .Build();
            RegisterHandlersAndConnect();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //throw new NotImplementedException();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageListBox.Items.Add(DateTime.Now + ": " + MessageTextBox.Text);
            _connection.InvokeAsync("SendMessageToAllClients", MessageTextBox.Text);
            MessageTextBox.Text = "";
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && !string.IsNullOrEmpty(MessageTextBox.Text))
            {
                SendButton_Click(null, null);
            }
        }
        private void AddMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                MessageListBox.Items.Add(DateTime.Now.TimeOfDay + ": " + message);
                MessageListBox.ScrollIntoView(MessageListBox.Items[MessageListBox.Items.Count - 1]);
            });
        }
        private void EditLastMessage(string message)
        {
            Dispatcher.Invoke(() =>
            {
                MessageListBox.Items[MessageListBox.Items.Count-1] = DateTime.Now.TimeOfDay + ": " + message;
                MessageListBox.ScrollIntoView(MessageListBox.Items[MessageListBox.Items.Count - 1]);
            });
        }
        private async void RegisterHandlersAndConnect()
        {
            string url = Environment.GetEnvironmentVariable("SB_SignalRHubUrl");
            if (string.IsNullOrEmpty(url))
                url = "http://localhost:53353/sbmessagehub";
            _connection = new HubConnectionBuilder().WithUrl(url).WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) }).Build();

            _connection.Closed += async (error) =>
            {
                //await reconnection logic
                bool isFirstTime = true;
                int delay = 0;
                while (_connection.State != HubConnectionState.Connected)
                {
                    try
                    {
                        delay = new Random().Next(0, 10);
                        AddMessage("Connection closed - " + error?.Message + ". Will retry in " + delay + "s");
                        await Task.Delay(delay * 1000);
                        await _connection.StartAsync();
                    }
                    catch (Exception e)
                    {
                        if (isFirstTime)
                        {
                            AddMessage("Error - " + e.Message);
                            isFirstTime = false;
                        }
                        else
                            EditLastMessage("Error connecting after a delay of " + delay + "s. Error - " + e.Message);
                    }
                }
            };
            _connection.Reconnecting += error =>
            {
                AddMessage("Reconnecting : " + error?.Message);
                return Task.CompletedTask;
            };
            _connection.Reconnected += connectionId =>
            {
                AddMessage("Connected Successfully with connectionID - " + connectionId);
                return Task.CompletedTask;
            };

            string topic = Environment.GetEnvironmentVariable("SB_SignalRHubTopic");
            if (string.IsNullOrEmpty(topic))
                topic = "MessageTopic";
            _connection.On<string>(topic, (message) =>
                  {
                      AddMessage(message);
                  });
            try
            {
                AddMessage("Trying to subscribe to " + topic + " on " + url);
                await _connection.StartAsync();
                if (_connection.State == HubConnectionState.Connected)
                {
                    AddMessage("Connected");
                }
            }
            catch (Exception e)
            {
                AddMessage("Could not connect - " + e.Message);
            }
        }


    }
}
