using log4net;
using Newtonsoft.Json;
using System;
using System.IO;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Topshelf;
using Topshelf.Runtime;
using SmartlifeCRMIntegration.HTTP;
using SmartlifeCRMIntegration.Models;
using SmartlifeCRMIntegration.Proxy;
using SmartlifeCRMIntegration.Service;
using System.Collections.Generic;

namespace SmartlifeCRMIntegration
{
    public class ServiceManager : ServiceControl
    {
        ServiceHost host;
        private readonly ILog logger;
        ClientControllerProxy client = null;
        System.Timers.Timer time = new System.Timers.Timer();
        int current_status = 0;

        public ServiceManager(HostSettings hostsetting)
        {
            try
            {
                logger = LogManager.GetLogger(GetType());
                Initialize();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                //StoreTransportationService.Utillities.Log<Service>.Error("Host", "Service", "Start Host", ex);
            }
        }
        #region Initial Server
        public virtual void Initialize()
        {
            try
            {

                #region initial Host Rest WCF Server.
                host = new ServiceHost(typeof(OperationService));
                host.Closed += Host_Closed;
                host.Closing += Host_Closing;
                host.Faulted += Host_Faulted;
                host.Opened += Host_Opened;
                host.Opening += Host_Opening;
                host.UnknownMessageReceived += Host_UnknownMessageReceived;
                #endregion

                #region Initial Client Controller Proxy
                ClientControllerProxy.ChannelFactoryOpened = ChannelFactoryOpened;
                ClientControllerProxy.ChannelFactoryFaulted = ChannelFactoryFaulted;
                ClientControllerProxy.ChannelFactoryClosed = ChannelFactoryClosed;
                ClientControllerProxy.ChannelInnerClosed = ChannelInnerClosed;
                ClientControllerProxy.ChannelInnerDuplexClosed = ChannelInnerDuplexClosed;

                ClientControllerProxy.InsertedOperation = InsertedOperation;
                ClientControllerProxy.UpdatedOperation = UpdatedOperation;
                ClientControllerProxy.DeletedOperation = DeletedOperation;
                #endregion

                time.Interval = 3000;
                time.Enabled = true;
                time.Start();
                time.Elapsed += Time_Elapsed;
            }
            catch (Exception e)
            {
                logger.Error("Server initialization failure:" + e.Message, e);
            }
        }

        private void Time_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                time.Stop();
                var status = ClientControllerProxy.getStatusClient();

                if (status == 0)
                {
                    if (current_status != status)
                    {
                        Console.WriteLine("Client Connection Fail.");
                    }
                    //Thread.Sleep(1000);
                    try
                    {
                        if (client != null) client = null;
                        client = new ClientControllerProxy();

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message, ex);
                    }
                }
                else
                {
                    if (current_status != status)
                    {
                        Console.WriteLine("Client Connected!");
                        current_status = status;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Set Status", ex);
            }
            finally
            {
                time.Start();
            }
        }
        #endregion




        public bool Start(HostControl hostControl)
        {
            try
            {
                host.Open();
                //scheduler.Start();
            }
            catch (Exception ex)
            {
                logger.Fatal(string.Format("Service start failure: {0}", ex.Message), ex);
                throw (ex);
            }
            logger.Info("Service started successfully");
            Console.WriteLine("Service started successfully");
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            try
            {
                host.Close();
                //scheduler.Shutdown();
                host = null;
                //scheduler = null;
            }
            catch (Exception ex)
            {
                logger.Fatal(string.Format("Scheduler stop failure: {0}", ex.Message), ex);
                throw (ex);
            }
            logger.Info("Service stop successfully");
            return true;
        }

        #region Events implementation
        private void Host_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {

        }

        private void Host_Opening(object sender, EventArgs e)
        {

        }

        private void Host_Opened(object sender, EventArgs e)
        {

        }

        private void Host_Faulted(object sender, EventArgs e)
        {

        }

        private void Host_Closing(object sender, EventArgs e)
        {

        }

        private void Host_Closed(object sender, EventArgs e)
        {

        }
        #endregion 

        #region Events Update Message

        void InsertedOperation(object sender, ClientControllerProxy.CallRequestEventArgs e)
        {
            try
            {

                // Check CONNECTION
                if (e.Message.EntityName == "CONNECTION")
                {
                    Tracklog("Inserted", e);
                    if (!Cache.getInstance().ContainsKey(e.Message.RecID)){
                        Cache.getInstance().Add(e.Message.RecID, e.Message);
                    }
                    ProcessMessage("Inserted", Cache.getInstance(), e.Message);
                }
                else if (e.Message.EntityName == "REGISTRATION")
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error("InsertedOperation", ex); 
            }
        }

        public bool IsValidPhone(string Phone)
        {
            try
            {
                if (string.IsNullOrEmpty(Phone))
                    return false;
                var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");
                return r.IsMatch(Phone);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            }
        }


        void UpdatedOperation(object sender, ClientControllerProxy.CallRequestEventArgs e)
        {
            try
            {

                if (e.Message.EntityName == "CONNECTION")
                {

                    Tracklog("Updated", e);

                    if (Cache.getInstance().ContainsKey(e.Message.RecID))
                    {
                        Cache.getInstance()[e.Message.RecID] = e.Message;
                    }
                    ProcessMessage("Updated", Cache.getInstance(), e.Message);

                    if (e.Message.Status == "Connected")
                    {

                    }
                }
                else if (e.Message.EntityName == "REGISTRATION")
                {

                }
            }
            catch (Exception ex)
            {
                logger.Error("UpdatedOperation", ex); 
            }
        }

        void DeletedOperation(object sender, ClientControllerProxy.CallRequestEventArgs e)
        {
            try
            {

                if (e.Message.EntityName == "CONNECTION")
                {
                    string dn = null;
                    try
                    {
                        string[] dnsp = e.Message.DN.Split(' ');
                        dn = (dnsp[0].IndexOf("Ext.") != -1) ? dnsp[0].Substring(dnsp[0].IndexOf("Ext.") + 4, dnsp[0].Length - (dnsp[0].IndexOf("Ext.") + 4)) : dnsp[0];
                        Tracklog("Deleted", e);

                        if (Cache.getInstance().ContainsKey(e.Message.RecID))
                        {
                            Cache.getInstance().Remove(e.Message.RecID);
                        }
                        ProcessMessage("Deleted", Cache.getInstance(), e.Message);
                    }
                    catch (Exception ex) {
                        logger.Error("DeletedOperation : " + ex.Message, ex);
                    }
                }

                else if (e.Message.EntityName == "REGISTRATION")
                {

                }

            }
            catch (Exception ex)
            {
                logger.Error("DeletedOperation", ex); 
            }
        }

        #endregion


        void ProcessMessage(string state, IDictionary<string, Object> cache, MessageObj current) {
            //Console.WriteLine(current.RecID + "RecID|State|Status|DN|InternalParty|ExternalParty");
            //foreach (var data in cache) {
            //    Console.WriteLine("" + data.Key + "|" + state + "|" + ((MessageObj)data.Value).Status + "|" + ((MessageObj)data.Value).DN + "|" + ((MessageObj)data.Value).InternalParty + "|" + ((MessageObj)data.Value).ExternalParty);
            //}
            //Console.WriteLine("==============================");

            if (current.Status == "Connected" && state == "Updated") {
                //---Inbound 
                if (!Cache.getStateInstance().ContainsKey(current.CallID) && current.DN.IndexOf("Ext.") != -1 && IsValidPhone(current.ExternalParty) && current.InternalParty.Length == 4 && current.IsInbound == "True")
                {
                    if (!Cache.getStateInstance().ContainsKey(current.RecID))
                    {
                        Cache.getStateInstance().Add(current.RecID, current);
                        SendMessage(current);
                        Console.WriteLine("######################");
                        foreach (var fire in Cache.getStateInstance())
                        {
                            Console.WriteLine("Fire---> RecID : " + fire.Key + " DN " + ((MessageObj)fire.Value).DN + " InternalParty " + ((MessageObj)fire.Value).InternalParty + " ExternalParty " + ((MessageObj)fire.Value).ExternalParty + " In/Out " + ((MessageObj)fire.Value).IsInbound + "/" + ((MessageObj)fire.Value).IsOutbound);
                            logger.Info("Fire---> RecID : " + fire.Key + " DN " + ((MessageObj)fire.Value).DN + " InternalParty " + ((MessageObj)fire.Value).InternalParty + " ExternalParty " + ((MessageObj)fire.Value).ExternalParty + " In/Out " + ((MessageObj)fire.Value).IsInbound + "/" + ((MessageObj)fire.Value).IsOutbound);
                        }
                        Console.WriteLine("######################");
                    }

                }
                //---Outbound 
                else if (!Cache.getStateInstance().ContainsKey(current.CallID) && current.DN.IndexOf("Ext.") != -1 && IsValidPhone(current.ExternalParty) && current.IsOutbound == "True") {
                    if (!Cache.getStateInstance().ContainsKey(current.RecID))
                    {
                        Cache.getStateInstance().Add(current.RecID, current);
                        SendMessage(current);
                        Console.WriteLine("######################");
                        foreach (var fire in Cache.getStateInstance())
                        {
                            Console.WriteLine("Fire---> RecID : " + fire.Key + " DN " + ((MessageObj)fire.Value).DN + " InternalParty " + ((MessageObj)fire.Value).InternalParty + " ExternalParty " + ((MessageObj)fire.Value).ExternalParty + " In/Out " + ((MessageObj)fire.Value).IsInbound + "/" + ((MessageObj)fire.Value).IsOutbound);
                            logger.Info("Fire---> RecID : " + fire.Key + " DN " + ((MessageObj)fire.Value).DN + " InternalParty " + ((MessageObj)fire.Value).InternalParty + " ExternalParty " + ((MessageObj)fire.Value).ExternalParty + " In/Out " + ((MessageObj)fire.Value).IsInbound + "/" + ((MessageObj)fire.Value).IsOutbound);
                        }
                        Console.WriteLine("######################");
                    }

                }
            }
            else if (state == "Deleted") {
                if (Cache.getStateInstance().ContainsKey(current.RecID))
                {
                    Cache.getStateInstance().Remove(current.RecID);
                    Console.WriteLine("######################");
                    foreach (var fire in Cache.getStateInstance())
                    {
                        Console.WriteLine("Fire---> RecID : " + fire.Key + " DN " + ((MessageObj)fire.Value).DN + " InternalParty " + ((MessageObj)fire.Value).InternalParty + " ExternalParty " + ((MessageObj)fire.Value).ExternalParty + " In/Out " + ((MessageObj)fire.Value).IsInbound + "/" + ((MessageObj)fire.Value).IsOutbound);
                        logger.Info("Fire---> RecID : " + fire.Key + " DN " + ((MessageObj)fire.Value).DN + " InternalParty " + ((MessageObj)fire.Value).InternalParty + " ExternalParty " + ((MessageObj)fire.Value).ExternalParty + " In/Out " + ((MessageObj)fire.Value).IsInbound + "/" + ((MessageObj)fire.Value).IsOutbound);
                    }
                    Console.WriteLine("######################");
                }
            }
        }

        void SendMessage(MessageObj Message) {
            #region Start thread
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                try {
                    //RealAssetGET get = new RealAssetGET(Message);
                    //if (System.Configuration.ConfigurationManager.AppSettings["DoFire"] == "true")
                    //{
                    //    logger.Info(get.DoGet());
                    //}
                    LineNotifyPOST get = new LineNotifyPOST(Message);
                    if (System.Configuration.ConfigurationManager.AppSettings["DoFire"] == "true")
                    {
                        logger.Info(get.DoPost());
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Connected 02 : " + ex.Message, ex);
                }
            }).Start();
            #endregion
        }


        void Tracklog(string message, ClientControllerProxy.CallRequestEventArgs e) {
            logger.Info(message + JsonConvert.SerializeObject(e.Message));
        }

        void ChannelInnerClosed(object sender, EventArgs e)
        {
            //MessageBox.Show("ChannelInnerClosed Closed"); 
        }

        void ChannelInnerDuplexClosed(object sender, EventArgs e)
        {
            //MessageBox.Show("ChannelInnerDuplexClosed Closed"); 
        }

        void ChannelFactoryOpened(object sender, EventArgs e)
        {
            //MessageBox.Show("Client Opened");
            //return;
        }

        void ChannelFactoryFaulted(object sender, EventArgs e)
        {
            //MessageBox.Show("Faulted Opened");
        }

        void ChannelFactoryClosed(object sender, EventArgs e)
        {
            //MessageBox.Show("Closed Opened");
        }


    }
}
