using SmartlifeCRMIntegration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartlifeCRMIntegration.HTTP
{
    class LineNotifyPOST : SendMessage
    {
        public const string TOKEN = "ahdg3fseWgbSKkqGOMlZ0gH5w35MUjlyvPZCipNdbA3";
        public LineNotifyPOST(MessageObj Message) : base(Message)
        {
            Ticket.procId = Message.IsInbound == "True" ? "1" : "2";
            Ticket.queueId = Message.InternalParty;
            GetTicketInPettern();
        }

        public string DoPost()
        {
            try
            {
                string s = GetPatternParam.Replace("&", "|");
                logger.Info(s);
                PostLineNotify(s);
                return "OK";
            }
            catch (Exception ex) {
                logger.Info(ex.Message);
                return ex.Message;
            }
        }
    }
}
