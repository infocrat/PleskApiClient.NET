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

        /// <summary>
        /// The get operation is used to retrieve all customers accounts settings from the database.
        /// </summary>
        /// <returns></returns>
        public CustomerGetResponse Get()
        {
            XElement message =
                new XElement("customer",
                    new XElement("get",
                        new XElement("filter"),
                        new XElement("dataset",
                            new XElement("gen_info"), 
                            new XElement("stat")
                        )
                    )
                );

            return CustomerGetResponse.Parse(ApiHttpClient.SendPacket(message));
        }

        /// <summary>
        /// The get operation is used to retrieve customer account settings from the database.
        /// </summary>
        /// <param name="id">Specifies ID of a customer account.</param>
        /// <param name="login">Specifies login name of a customer account.</param>
        /// <param name="ownerId">Specifies ID of a customer account owner.</param>
        /// <param name="ownerLogin">Specifies login name of a customer account owner.</param>
        /// <param name="guid">Specifies the GUID of a customer account. For details on GUIDs, refer to the API RPC Protocol > GUIDs Overview section of Plesk API RPC Developer's Guide.</param>
        /// <param name="getGenInfo">Need to request for the general customer account settings.</param>
        /// <param name="getStat">Need to request statistics on the specified customers.</param>
        /// <returns></returns>
        public CustomerGetResponse Get(int? id = null, string login = null, int? ownerId = null, string ownerLogin = null, string guid = null, bool getGenInfo = true, bool getStat = false)
        {
            XElement message =
                new XElement("customer",
                    new XElement("get",
                        new XElement("filter",
                            id.HasValue ?                            new XElement("id", id.Value) : null,
                            !String.IsNullOrWhiteSpace(login) ?      new XElement("login", login) : null,
                            ownerId.HasValue ?                       new XElement("owner-id", ownerId) : null,
                            !String.IsNullOrWhiteSpace(ownerLogin) ? new XElement("owner-login", ownerLogin) : null,
                            !String.IsNullOrWhiteSpace(guid) ?       new XElement("guid", guid) : null
                        ),
                        new XElement("dataset",
                            getGenInfo ?    new XElement("gen_info") : null,
                            getStat ?       new XElement("stat") : null
                        )
                    )
                );

            return CustomerGetResponse.Parse(ApiHttpClient.SendPacket(message));
        }

        private PleskApiHttpClient ApiHttpClient { get; }
    }
}
