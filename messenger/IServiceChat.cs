using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace messenger
{
    [ServiceContract(CallbackContract = typeof(ISCCB))]
    public interface IServiceChat
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int ID);

        /*[OperationContract]
        void SetConnUser(string name, int ID);*/

        [OperationContract(IsOneWay = true)]
        void SendMessage(string msg, int ID);

        [OperationContract(IsOneWay = true)]
        void SendConnDiscon(string msg);

        /*[OperationContract(IsOneWay = true)]
        void UpdateUsers(int ID, int mode);*/
    }

    public interface ISCCB
    {
        [OperationContract(IsOneWay = true)]
        void MessageCB(string msg, int mode);
    }
}
