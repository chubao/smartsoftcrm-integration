using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SmartlifeCRMIntegration.CallControllerService;
using SmartlifeCRMIntegration.Models;

namespace SmartlifeCRMIntegration.Proxy
{
    public class ClientControllerProxy : IServiceControllerCallback
    {
        private static ServiceControllerClient client = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(ClientControllerProxy));

        public delegate void InsertedRequestDelegate(object sender, CallRequestEventArgs e);
        public static InsertedRequestDelegate InsertedOperation { get; set; }

        public delegate void UpdatedRequestDelegate(object sender, CallRequestEventArgs e);
        public static UpdatedRequestDelegate UpdatedOperation { get; set; }

        public delegate void DeletedRequestDelegate(object sender, CallRequestEventArgs e);
        public static DeletedRequestDelegate DeletedOperation { get; set; }

        public delegate void ChannelFactoryOpenedDelegate(object sender, EventArgs e);
        public static ChannelFactoryOpenedDelegate ChannelFactoryOpened { get; set; }

        public delegate void ChannelFactoryFaultedDelegate(object sender, EventArgs e);
        public static ChannelFactoryFaultedDelegate ChannelFactoryFaulted { get; set; }

        public delegate void ChannelFactoryClosedDelegate(object sender, EventArgs e);
        public static ChannelFactoryClosedDelegate ChannelFactoryClosed { get; set; }

        public delegate void ChannelInnerClosedDelegate(object sender, EventArgs e);
        public static ChannelInnerClosedDelegate ChannelInnerClosed { get; set; }

        public delegate void ChannelInnerDuplexClosedDelegate(object sender, EventArgs e);
        public static ChannelInnerDuplexClosedDelegate ChannelInnerDuplexClosed { get; set; }

        JavaScriptSerializer serializer = new JavaScriptSerializer();

        static int statusConnect = 0;

        public class CallRequestEventArgs
        {
            public MessageObj Message { get; set; }
            public CallRequestEventArgs(MessageObj message)
            {
                Message = message;
            }
        }

        public ClientControllerProxy()
        {
            try
            {
                client = new ServiceControllerClient(new InstanceContext(this), "NetTcpBinding_IServiceController");
                //((ICommunicationObject)client).Closed += new EventHandler(ClientControllerProxy_Closed);

                //client.ChannelFactory.Opened += new EventHandler(ChannelFactory_Opened);
                //client.ChannelFactory.Opening += new EventHandler(ChannelFactory_Opening);
                //client.ChannelFactory.Faulted += new EventHandler(ChannelFactory_Faulted);
                //client.ChannelFactory.Closing += new EventHandler(ChannelFactory_Closing);
                //client.ChannelFactory.Closed += new EventHandler(ChannelFactory_Closed);
                //client.InnerChannel.Opened += new EventHandler(InnerChannel_Closed);
                //client.InnerChannel.Closed += new EventHandler(InnerChannel_Closed);
                //client.InnerDuplexChannel.Opened += new EventHandler(InnerDuplexChannel_Closed);
                //client.InnerDuplexChannel.Closed += new EventHandler(InnerDuplexChannel_Closed);

                //client.InnerDuplexChannel.Faulted += new EventHandler(InnerDuplexChannel_Faulted);
                //client.InnerChannel.Faulted += new EventHandler(InnerChannel_Faulted);

                client.Open();
                statusConnect = 1;
            }
            catch (Exception ex)
            {
                log.Error("Client.Open", ex);
                throw (ex);
            }
            try
            {
                string result = client.InitailState("Connected");
                if (result != "200 OK")
                {
                    log.Error("InitailState", new Exception(result));
                    throw (new Exception(result));
                }
            }
            catch (Exception ex)
            {
                log.Error("InitailState", ex);
                throw (ex);
            }
        }

        void InnerChannel_Faulted(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void InnerDuplexChannel_Faulted(object sender, EventArgs e)
        {
            var status = 0;
        }

        void ClientControllerProxy_Closed(object sender, EventArgs e)
        {
            statusConnect = 0;
        }

        public static int getStatusClient()
        {
            try
            {
                if (client == null)
                {
                    return 0;
                }
                else
                {
                    if (client != null && (client.State | CommunicationState.Opened) == CommunicationState.Opened)
                    {
                        string result = client.InitailState("Connected");
                        if (result == "200 OK")
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex) { return 0; }
        }

        void InnerDuplexChannel_Closed(object sender, EventArgs e)
        {
            try
            {
                ChannelInnerDuplexClosedDelegate handler = ChannelInnerDuplexClosed;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                log.Error("ChannelFactory_Opened", ex);
            }
        }

        void InnerChannel_Closed(object sender, EventArgs e)
        {
            try
            {
                ChannelInnerClosedDelegate handler = ChannelInnerClosed;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                log.Error("ChannelFactory_Opened", ex);
            }
        }

        void ChannelFactory_Closed(object sender, EventArgs e)
        {
            try
            {
                ChannelFactoryClosedDelegate handler = ChannelFactoryClosed;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                log.Error("ChannelFactory_Opened", ex);
            }
        }

        void ChannelFactory_Closing(object sender, EventArgs e)
        {

        }

        void ChannelFactory_Faulted(object sender, EventArgs e)
        {
            try
            {
                ChannelFactoryFaultedDelegate handler = ChannelFactoryFaulted;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                log.Error("ChannelFactory_Opened", ex);
            }
        }

        void ChannelFactory_Opening(object sender, EventArgs e)
        {

        }

        void ChannelFactory_Opened(object sender, EventArgs e)
        {
            try
            {
                ChannelFactoryOpenedDelegate handler = ChannelFactoryOpened;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                log.Error("ChannelFactory_Opened", ex);
            }
        }

        public void Inserted(string Message)
        {
            try
            {
                InsertedRequestDelegate handler = InsertedOperation;
                if (handler != null)
                {
                    var message = serializer.Deserialize<MessageObj>(Message);
                    handler(this, new CallRequestEventArgs((MessageObj)message));
                }
            }
            catch (Exception ex)
            {
                log.Error("Inserted", ex);
            }
        }

        public void Updated(string Message)
        {
            try
            {
                UpdatedRequestDelegate handler = UpdatedOperation;
                if (handler != null)
                {
                    var message = serializer.Deserialize<MessageObj>(Message);
                    handler(this, new CallRequestEventArgs((MessageObj)message));
                }
            }
            catch (Exception ex)
            {
                log.Error("Inserted", ex);
            }
        }

        public void Deleted(string Message)
        {
            try
            {
                DeletedRequestDelegate handler = DeletedOperation;
                if (handler != null)
                {
                    var message = serializer.Deserialize<MessageObj>(Message);
                    handler(this, new CallRequestEventArgs((MessageObj)message));
                }
            }
            catch (Exception ex)
            {
                log.Error("Inserted", ex);
            }
        }

        public void DropCall(int callID, string dn)
        {
            try
            {
                client.DropCall(callID, dn);
            }
            catch (Exception ex)
            {
                log.Error("DropCall", ex);
                throw (ex);
            }
        }

        public void WhisperTo(int callID, string dn, string whisper_by)
        {
            try
            {
                client.WhisperTo(callID, dn, whisper_by);
            }
            catch (Exception ex)
            {
                log.Error("WhisperTo", ex);
                throw (ex);
            }
        }

        public void Listen(int callID, string dn, string listen_by)
        {
            try
            {
                client.Listen(callID, dn, listen_by);
            }
            catch (Exception ex)
            {
                log.Error("WhisperTo", ex);
                throw (ex);
            }
        }

        public void MakeCall(string dn_from, string number_to)
        {
            try
            {
                client.MakeCall(dn_from, number_to);
            }
            catch (Exception ex)
            {
                log.Error("MakeCall", ex);
                throw (ex);
            }
        }

        public void MakeCallAsync(string dn_from, string number_to)
        {
            try
            {
                client.MakeCallAsync(dn_from, number_to);
            }
            catch (Exception ex)
            {
                log.Error("MakeCallAsync", ex);
                throw (ex);
            }
        }
    }
}
