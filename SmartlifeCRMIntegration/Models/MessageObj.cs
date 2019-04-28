using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartlifeCRMIntegration.Models
{
    public class MessageObj
    {
        public string RecID { get; set; }
        public string EntityName { get; set; }
        public string CallConnectionID { get; set; }
        public string CallID { get; set; }
        public string DialedNumber { get; set; }
        public string DN { get; set; }
        public string ExternalParty { get; set; }
        public string HistoryIDOfTheCall { get; set; }
        public string InternalParty { get; set; }
        public string IsInbound { get; set; }
        public string IsOutbound { get; set; }
        public string LastChangeStatus { get; set; }
        public string OriginatedBy { get; set; }
        public string ReferredBy { get; set; }
        public string Status { get; set; }
        public string chid { get; set; }
        public string prevCall { get; set; }
        public string prevLeg { get; set; }
        public string extnumber { get; set; }
        public string lookup_displayname { get; set; }
        public string sip_displayname { get; set; }
    }
}
