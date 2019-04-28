using log4net;
using Newtonsoft.Json;
using SmartlifeCRMIntegration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SmartlifeCRMIntegration.HTTP
{
    public abstract class SendMessage
    {
        protected string GetPatternParam;
        protected string Uri;
        protected MessageObj Message;
        protected Ticket Ticket;
        protected readonly ILog logger;
        public SendMessage(MessageObj Message) {
            logger = LogManager.GetLogger(GetType());
            Uri = System.Configuration.ConfigurationManager.AppSettings["URL"];
            GetPatternParam = System.Configuration.ConfigurationManager.AppSettings["GETPatternParam"];
            this.Message = Message;
            Ticket = new Ticket();
            try
            {
                Ticket.callerId = Message.ExternalParty;
                string[] internalPartys = Message.DN.Split(' ');
                Ticket.extensionId = (internalPartys[0].IndexOf("Ext.") != -1) ? internalPartys[0].Substring(internalPartys[0].IndexOf("Ext.") + 4, internalPartys[0].Length - (internalPartys[0].IndexOf("Ext.") + 4)) : internalPartys[0];
                Ticket.extensionName = (internalPartys.Length > 2) ? Message.DN.Substring(internalPartys[0].Length + 1) : internalPartys[0];
                Ticket.soundUrl = GetSoundURL();
                logger.Info(JsonConvert.SerializeObject(Ticket));
            }
            catch (Exception ex)
            {
                logger.Error("Connected 01 : " + ex.Message, ex);
            }

        }

        protected  string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        protected  async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        protected  string Post(string uri, string data, string contentType, string header = "", string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            if (header != "")
                request.Headers.Add(header);
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        protected  async Task<string> PostAsync(string uri, string data, string contentType, string method = "POST")
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = contentType;
            request.Method = method;

            using (Stream requestBody = request.GetRequestStream())
            {
                await requestBody.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        protected void PostLineNotify(string Message) {
            LineNotify send = new LineNotify();
            send.lineNotify(Message);
        }

        private string GetSoundURL() {
            try
            {
                FileInfo file = null;
                DirectoryInfo directory = null;
                string filename = "";
                directory = new DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["RecordingPath"] + "/" + Ticket.extensionId);
                directory.Refresh();
                int i = 0;
                while (filename.IndexOf(Message.CallID) == -1 && i < 3)
                {
                    Thread.Sleep(1000);
                    file = GetLatestWritenFileFileInDirectory(directory);
                    filename = file.Name;
                    Console.WriteLine("Filename:=" + filename);
                    logger.Info("Filename:=" + filename);
                    i++;
                }
                if (filename.IndexOf(Message.CallID) != -1)
                {
                    string stroriginal = String.Format(@"{0}/{1}", Ticket.extensionId, filename);
                    string str_url_encoding = HttpUtility.UrlEncode(stroriginal);
                    str_url_encoding = HttpUtility.UrlEncode(str_url_encoding);
                    Console.WriteLine("file encoding := " + str_url_encoding);
                    logger.Info("file encoding := " + str_url_encoding);
                    return System.Configuration.ConfigurationManager.AppSettings["PreFixURL"] + str_url_encoding;
                }
                else
                    return System.Configuration.ConfigurationManager.AppSettings["PreFixURL"] + HttpUtility.UrlEncode(HttpUtility.UrlEncode(String.Format(@"{0}/File not found!", Ticket.extensionId)));

            }
            catch (Exception ex) { return ex.Message; }
        }

        protected void GetTicketInPettern() {
            GetPatternParam = (Ticket.extensionId != null) ? GetPatternParam.Replace("{extensionId}", Ticket.extensionId): GetPatternParam;
            GetPatternParam = (Ticket.extensionName != null) ? GetPatternParam.Replace("{extensionName}", Ticket.extensionName) : GetPatternParam;
            GetPatternParam = (Ticket.procId != null) ? GetPatternParam.Replace("{procId}", Ticket.procId) : GetPatternParam;
            GetPatternParam = (Ticket.queueId != null) ? GetPatternParam.Replace("{queueId}", Ticket.queueId) : GetPatternParam;
            GetPatternParam = (Ticket.soundUrl != null) ? GetPatternParam.Replace("{soundUrl}", Ticket.soundUrl) : GetPatternParam;
            GetPatternParam = (Ticket.callerId != null) ? GetPatternParam.Replace("{callerId}", Ticket.callerId) : GetPatternParam;
            GetPatternParam = GetPatternParam.Replace("%26", "&");
            logger.Info("GetTicketInPettern = " + GetPatternParam);
        }

        private FileInfo GetLatestWritenFileFileInDirectory(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null || !directoryInfo.Exists)
                return null;
            directoryInfo.Refresh();
            FileInfo[] files = directoryInfo.GetFiles();

            DateTime lastWrite = DateTime.MinValue;
            FileInfo lastWritenFile = null;

            foreach (FileInfo file in files)
            {
                if (file.LastWriteTime > lastWrite)
                {
                    lastWrite = file.LastWriteTime;
                    lastWritenFile = file;
                }
            }
            return lastWritenFile;
        }

    }
}
