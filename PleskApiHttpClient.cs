using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace PleskApi
{
    public class PleskApiHttpClient
    {
        public string Host { get; }
        public string Login { get; }
        public PleskApiHttpClient(string host, string login, string password)
        {
            Host = host;
            Login = login;
            Password = password;
        }

        public XElement SendPacket(XElement body)
        {
            XElement message = new XElement("packet",
                                    new XAttribute("version", "1.6.7.0"),
                                    body
                                );
            return GetResponse(Request(message));
        }

        public HttpWebRequest Request(XElement message)
        {
            return Request(message.ToString());
        }

        public HttpWebRequest Request(string message)
        {
            //ServicePointManager.ServerCertificateValidationCallback =
            //            new RemoteCertificateValidationCallback(RemoteCertificateValidation);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AgentEntryPoint);

            request.Method = "POST";
            request.Headers.Add("HTTP_AUTH_LOGIN", Login);
            request.Headers.Add("HTTP_AUTH_PASSWD", Password);
            request.ContentType = "text/xml";
            request.ContentLength = message.Length;

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] buffer = encoding.GetBytes(message);

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(buffer, 0, message.Length);
            }

            return request;
        }

        public XElement GetResponse(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (TextReader reader = new StreamReader(stream))
            {
                return XElement.Parse(reader.ReadToEnd().Trim());
            }
        }

        private string Password { get; }
        private string AgentEntryPoint { get { return "https://" + Host + ":8443/enterprise/control/agent.php"; } }

        private static bool RemoteCertificateValidation(object sender,
              X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors != SslPolicyErrors.RemoteCertificateNotAvailable)
                return true;

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }
}
