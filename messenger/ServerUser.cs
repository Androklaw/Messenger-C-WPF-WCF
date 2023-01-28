using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace messenger
{
    class ServerUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string connectedUser { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
