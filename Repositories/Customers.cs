using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using PleskApi.Models;

namespace PleskApi
{
    /// <summary>
    /// Customers repository and API endpoint
    /// </summary>
    public class Customers
    {
        public Customers(PleskApiHttpClient httpClient)
        {
            ApiHttpClient = httpClient;
        }

        public CustomerAddResponse Add(Customer customer)
        {
            XElement message =
                new XElement("customer",
                    new XElement("add",
                        new XElement("gen_info", 
                            customer.ToXml(ModelXmlDataType.CustomerAdd)
                        )
                    )
                );
            return CustomerAddResponse.Parse(ApiHttpClient.SendPacket(message));
        }

        public CustomerGetResponse Get()
        {
            XElement message =
                new XElement("customer",
                    new XElement("get",
                        new XElement("filter"),
                        new XElement("dataset",
                            new XElement("gen_info")
                        )
                    )
                );

            return CustomerGetResponse.Parse(ApiHttpClient.SendPacket(message));
        }

        private PleskApiHttpClient ApiHttpClient { get; }
    }
}
