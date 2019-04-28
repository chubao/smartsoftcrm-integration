using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartlifeCRMIntegration.Models
{
    public class Ticket
    {
        public string callerId { get; set; }
        public string extensionId { get; set; }
        public string extensionName { get; set; }
        public string soundUrl { get; set; }
        public string queueId { get; set; }
        public string procId { get; set; }
    }
}
