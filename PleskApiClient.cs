using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Threading.Tasks;
//using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace PleskApi
{
    public class PleskApiClient
    {
        public PleskApiHttpClient ApiHttpClient { get; set; }

        public PleskApiClient(string host, string login, string password)
        {
            ApiHttpClient = new PleskApiHttpClient(host, login, password);

            Subscriptions = new Subscriptions(ApiHttpClient);
            Customers = new Customers(ApiHttpClient);
        }

        /// <summary>
        /// Subscriptions repository and API endpoint
        /// </summary>
        public Subscriptions Subscriptions { get; set; }

        /// <summary>
        /// Customers repository and API endpoint
        /// </summary>
        public Customers Customers { get; set; }

        public string Test()
        {
            //string packet = "<packet><customer><get><filter><site-name>uralces.ru</site-name></filter></get></customer></packet>";
            string packet = "<packet><customer><get><filter/><dataset><gen_info/></dataset></get></customer></packet>";
            HttpWebRequest request = ApiHttpClient.Request(packet);
            XElement result = ApiHttpClient.GetResponse(request);
            foreach (XElement node in result.Elements("login"))
            {
                Console.WriteLine(node.ToString());
            }
            return "";
        }

        public CustomerAddResponse CustomerAdd(string email, string password)//, string domain = null, string plan = null)
        {
            XElement message = new XElement("packet",
                            new XAttribute("version", "1.6.7.0"),
                            new XElement("customer",
                                new XElement("add",
                                    new XElement("gen_info",
                                        new XElement("pname", email),
                                        new XElement("login", email),
                                        new XElement("passwd", password),
                                        new XElement("email", email)
                                    )
                                )
                            )
            );
            HttpWebRequest request = ApiHttpClient.Request(message);
            XElement resp = ApiHttpClient.GetResponse(request);

            return CustomerAddResponse.Parse(resp);
        }

        public CustomerGetResponse CustomerGet(string login = "")
        {
            XElement message = new XElement("packet",
                            new XAttribute("version", "1.6.7.0"),
                            new XElement("customer",
                                new XElement("get",
                                    new XElement("filter",
                                        new XElement("login", login)
                                    ),
                                    new XElement("dataset",
                                        new XElement("gen_info")
                                    )
                                )
                            )
            );

            HttpWebRequest request = ApiHttpClient.Request(message);
            XElement resp = ApiHttpClient.GetResponse(request);

            return CustomerGetResponse.Parse(resp);
        }

        public bool CustomerIsExist(string email)
        {
            CustomerGetResponse resp = CustomerGet(email);

            return resp.Status == PleskApiResponseStatus.Ok;
        }

/*        public SubscriptionAddResponse SubscriptionAdd(string name, string ownerGuid, IPAddress ip, string planName)
        {
            XElement message = new XElement("packet",
                            //new XAttribute("version", "1.6.7.0"),
                            new XElement("webspace",
                                new XElement("add",
                                    new XElement("gen_setup",
                                        new XElement("name", name),
                                        //new XElement("owner-guid", ownerGuid),
                                        new XElement("htype", "vrt_hst"),
                                        new XElement("ip_address", ip),
                                        new XElement("status", 0)
                                    ),
                                    new XElement("hosting",
                                        new XElement("vrt_hst", 
                                            new XElement("property",
                                                new XElement("name", "ssl"),
                                                new XElement("value", true)
                                            ),
                                            new XElement("property",
                                                new XElement("name", "ftp_login"),
                                                new XElement("value", "testlogin")
                                            ),
                                            /*new XElement("property",
                                                new XElement("name", "ftp_password"),
                                                new XElement("value", "qweqwe123")
                                            ),*/
                                            /*new XElement("property",
                                                new XElement("name", "ftp_password"),
                                                new XElement("value", "qweqwe")
                                            ),
                                            /*new XElement("ip_address",ip)
                                        )
                                    ),
                                    new XElement("plan-name", planName)
                                )
                            )
            );
            HttpWebRequest request = ApiHttpClient.Request(message);
            XElement resp = ApiHttpClient.GetResponse(request);

            return SubscriptionAddResponse.Parse(resp);
        }*/

        public SessionCreateResponse SessionCreate(string login, IPAddress userIp)
        {
            XElement message = new XElement("packet",
                            new XAttribute("version", "1.6.7.0"),
                            new XElement("server",
                                new XElement("create_session",
                                    new XElement("login", login),
                                    new XElement("data",
                                        new XElement("user_ip", userIp),
                                        new XElement("source_server")
                                    )
                                )
                            )
            );
            HttpWebRequest request = ApiHttpClient.Request(message);
            XElement resp = ApiHttpClient.GetResponse(request);

            return SessionCreateResponse.Parse(resp);
        }
    }
}
