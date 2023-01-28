using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace messenger
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextID = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextID,
                Name = name,
                connectedUser = null,
                operationContext = OperationContext.Current
            };
            nextID++;

            SendConnDiscon(user.Name + " подключился к чату!");
            users.Add(user);
            //UpdateUsers(user.ID, 1);
            return user.ID;
        }

        public void Disconnect(int ID)
        {
            var user = users.FirstOrDefault(x => x.ID == ID);
            if (user != null)
            {
                //UpdateUsers(user.ID, 1);
                SendConnDiscon(user.Name + " вышел из чата!");
                users.Remove(user);
            }
        }

        public void SendConnDiscon(string msg)
        {
            //if(users.Count != 0)
            //{
                foreach (var item in users)
                {
                    if (item.connectedUser == null)
                    {
                        string time = DateTime.Now.ToShortTimeString();
                        string message = "(" + time + "): " + msg;

                        item.operationContext.GetCallbackChannel<ISCCB>().MessageCB(message, 1);
                    }
                }
            //}
        }

        public void SendMessage(string msg, int ID)
        {
            var user = users.FirstOrDefault(x => x.ID == ID);
            if(user.connectedUser == null)
            {
                string time = DateTime.Now.ToShortTimeString();
                string message = "(" + time + "): " + "Error: Что бы кому-то что-то писать, выберите юзера";

                user.operationContext.GetCallbackChannel<ISCCB>().MessageCB(message, 1);
            }
            foreach (var item in users)
            {
                if (item.Name == user.Name && item.Name == user.connectedUser && item.connectedUser == user.Name)
                {
                    string time = DateTime.Now.ToShortTimeString();
                    string message = "(" + time + ") " + user.Name + ": " + msg;

                    item.operationContext.GetCallbackChannel<ISCCB>().MessageCB(message, 1);
                }
                else
                {
                    continue;
                }
            }
        }

        /*public void SetConnUser(string name, int ID)
        {
            var user = users.FirstOrDefault(x => x.ID == ID);
            user.connectedUser = name;
        }*/

        /*public void UpdateUsers(int ID, int mode)
        {
            var user = users.FirstOrDefault(x => x.ID == ID);
            foreach (var item in users)
            {
                if(user != item)
                {
                    item.operationContext.GetCallbackChannel<ISCCB>().MessageCB(user.Name, mode);
                    user.operationContext.GetCallbackChannel<ISCCB>().MessageCB(item.Name, mode);
                }
            }
        }*/
    }
}
