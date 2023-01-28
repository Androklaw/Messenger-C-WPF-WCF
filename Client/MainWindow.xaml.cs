using System.Windows;
using System.Windows.Input;

namespace Client
{
    public partial class MainWindow : Window, ServiceChat.IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChat.ServiceChatClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if(!isConnected)
            {
                client = new ServiceChat.ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                lbChat.IsEnabled = true;
                lbUsers.IsEnabled = true;
                tbMessage.IsEnabled = true;
                tbUserName.IsEnabled = false;
                ID = client.Connect(tbUserName.Text);
                isConnected = true;
                bConnect.Content = "Disconnect";
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                tbMessage.Text = string.Empty;
                //lbChat.Items.Clear();
                //lbUsers.Items.Clear();
                lbChat.IsEnabled = false;
                lbUsers.IsEnabled = false;
                tbMessage.IsEnabled = false;
                isConnected = false;
                bConnect.Content = "Connect";
            }
        }

        private void bConnect_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCB(string msg, int mode)
        {
            if (mode == 1)
            {
                lbChat.Items.Add(msg);
                lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
            }
            if (mode == 2)
            {
                lbUsers.Items.Add(msg);
            }
            if (mode == 3)
            {
                lbUsers.Items.Remove(msg);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(client != null)
                {
                    client.SendMessage(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
            }
        }

        /*private void lbUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lbUsers.SelectedItem != null)
            {
                client.SetConnUser(lbUsers.SelectedItem.ToString(), ID);
                lbChat.Items.Clear();
                tbMessage.Text = string.Empty;
            }
        }*/
    }
}
