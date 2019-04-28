using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using SmartlifeCRMIntegration.Interfaces;
using SmartlifeCRMIntegration.Models;
using SmartlifeCRMIntegration.Proxy;

namespace SmartlifeCRMIntegration.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class OperationService : IOperationService
    {
        public ReturnModel MakeCall(string dn_from, string number_to)
        {
            try
            {
                ClientControllerProxy client = new ClientControllerProxy();
                client.MakeCall(dn_from, number_to);
                client = null;
                ReturnModel result = new ReturnModel();
                result.Result = "200";
                result.Message = "OK";
                return result;
            }
            catch (Exception ex) {
                ReturnModel result = new ReturnModel();
                result.Result = "500";
                result.Message = "Error : " + ex.Message;
                return result;
            }

        }

        public ReturnModel MakeCallAsync(string dn_from, string number_to)
        {
            try
            {
                ClientControllerProxy client = new ClientControllerProxy();
                client.MakeCallAsync(dn_from, number_to);
                client = null;
                ReturnModel result = new ReturnModel();
                result.Result = "200";
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                ReturnModel result = new ReturnModel();
                result.Result = "500";
                result.Message = "Error : " + ex.Message;
                return result;
            }
        }
    }
}
