using SmartlifeCRMIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartlifeCRMIntegration.HTTP
{
    public class RealAssetGET : SendMessage
    {
        public RealAssetGET(MessageObj Message) : base(Message)
        {
            Ticket.procId = Message.IsInbound == "True" ? "1" : "2";
            Ticket.queueId = Message.InternalParty;
            GetTicketInPettern();
        }

        public string DoGet() {
            String GetUri = Uri + GetPatternParam;
            logger.Info(GetUri);
            var r = Get(GetUri);
            return r.ToString();
        }

        
        
    }
}
