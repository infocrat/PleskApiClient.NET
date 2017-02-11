using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EnumAnnotations;

using PleskApi.Models;

namespace PleskApi
{
    /// <summary>
    /// Subscriptions repository and API endpoint
    /// </summary>
    public class Subscriptions
    {
        public Subscriptions(PleskApiHttpClient httpClient)
        {
            ApiHttpClient = httpClient;
        }

        public SubscriptionAddResponse Add(Subscription subscription)
        {
            XElement message =
                            new XElement("webspace",
                                new XElement("add",
                                    new XElement("gen_setup",
                                        new XElement("name", subscription.Name),
                                        new XElement("owner-guid", subscription.OwnerGuid),
                                        new XElement("htype", subscription.HostingType.GetName().ToString()),
                                        new XElement("ip_address", subscription.IPAddress),
                                        new XElement("status", subscription.Status)
                                    )
                                )
                            );

            switch (subscription.HostingType)
            {
                case HostingType.Virtual:
                    VirtualHostingSettings vhsettings = (VirtualHostingSettings)subscription.HostingSettings;

                    /*foreach (var p in vhsettings.GetType().GetProperties().Where(p => p.CanWrite))
                    {
                        Console.WriteLine(p.GetCustomAttribute<DisplayAttribute>().ShortName);
                    }*/
                    XElement vhost = new XElement("hosting",
                                        new XElement("vrt_hst"//,
                                            /*vhsettings.GetType().GetProperties().Where(p => p.CanWrite).Select(p =>
                                                BuildPropertyXml(p.GetCustomAttribute<DisplayAttribute>().ShortName, p.GetValue(vhsettings))
                                            )*/
                    //                    new XElement("ip_address", vhsettings.IPAddress)
                                        //vhsettings.Properties.Select(x => BuildPropertyXml(x.Key, x.Value))
                                        )
                                    );

                    if (!String.IsNullOrWhiteSpace(vhsettings.FtpLogin))
                        vhost.Descendants("vrt_hst").FirstOrDefault().Add(BuildPropertyXml("ftp_login", vhsettings.FtpLogin));

                    if (!String.IsNullOrWhiteSpace(vhsettings.FtpPassword))
                        vhost.Descendants("vrt_hst").FirstOrDefault().Add(BuildPropertyXml("ftp_password", vhsettings.FtpPassword));

                    vhost.Descendants("vrt_hst")
                        .FirstOrDefault()
                        .Add(new XElement("ip_address", ((VirtualHostingSettings)subscription.HostingSettings).IPAddress));

                    if (message.Descendants("add").Any())
                        message.Descendants("add").FirstOrDefault().Add(vhost);
                    break;
                default:
                    break;
            }

            //Adding information about plans if exists
            if (message.Descendants("add").Any())
            {
                if (subscription.PlanId.HasValue)
                    message.Descendants("add").FirstOrDefault().Add(new XElement("plan-id", subscription.PlanId));
                if (!String.IsNullOrWhiteSpace(subscription.PlanName))
                    message.Descendants("add").FirstOrDefault().Add(new XElement("plan-name", subscription.PlanName));
                if (!String.IsNullOrWhiteSpace(subscription.PlanGuid))
                    message.Descendants("add").FirstOrDefault().Add(new XElement("plan-guid", subscription.PlanGuid));
                if (!String.IsNullOrWhiteSpace(subscription.PlanExternalId))
                    message.Descendants("add").FirstOrDefault().Add(new XElement("plan-external-id", subscription.PlanExternalId));
            }

            //Console.WriteLine(message.ToString());
            return SubscriptionAddResponse.Parse(ApiHttpClient.SendPacket(message));
        }



        private PleskApiHttpClient ApiHttpClient { get; }

        private XElement BuildPropertyXml(string key, object value)
        {
            return
                new XElement("property",
                    new XElement("name", key),
                    new XElement("value", value)
                );
        }
    }
}
