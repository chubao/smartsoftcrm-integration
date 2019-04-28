using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using SmartlifeCRMIntegration.Models;

namespace SmartlifeCRMIntegration.Interfaces
{
    [ServiceContract]
    public interface IOperationService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "MakeCall/{dn_from}&{number_to}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RemoteOperation/{Message}")]
        //[return: MessageParameter(Name = "Output")]
        ReturnModel MakeCall(string dn_from, string number_to);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "MakeCallAsync/{dn_from}&{number_to}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "RemoteOperation/{Message}")]
        //[return: MessageParameter(Name = "Output")]
        ReturnModel MakeCallAsync(string dn_from, string number_to);
    }
}
