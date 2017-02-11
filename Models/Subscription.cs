using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace PleskApi.Models
{
    public class Subscription
    {
        /// <summary>
        /// Specifies a subscription name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Specifies ID of a Plesk user (subscriptions owner).
        /// </summary>
        public int? OwnerId { get; set; } //optional

        /// <summary>
        /// Specifies the Plesk user login name.
        /// </summary>
        public string OwnerLogin { get; set; } //optional

        /// <summary>
        /// Specifies the Plesk user GUID. 
        /// </summary>
        public string OwnerGuid { get; set; } //optional

        /// <summary>
        /// Specifies the ID of a Plesk user in other components or applications.
        /// </summary>
        public string OwnerExternalId { get; set; } //optional

        /// <summary>
        /// Specifies one of the following hosting types: virtual hosting, standard forwarding, frame forwarding, none.
        /// </summary>
        public HostingType HostingType { get; set; }

        /// <summary>
        /// Specifies the IP address associated with the site.
        /// </summary>
        [Required]
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// Specifies the status of the created site.
        /// </summary>
        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

        /// <summary>
        /// Specifies a GUID of a subscription owner received from the Plesk components (f.e., Business Manager).
        /// </summary>
        public string ExternalId { get; set; }

        public HostingSettings HostingSettings { get; set; }

        /// <summary>
        /// Specifies the service plan by ID if it is necessary to create a subscription to a certain service plan.
        /// </summary>
        public int? PlanId { get; set; }

        /// <summary>
        /// Specifies the service plan by name if it is necessary to create a subscription to a certain service plan.
        /// </summary>
        public string PlanName { get; set; }

        /// <summary>
        /// Specifies the service plan by GUID if it is necessary to create a subscription to a certain service plan.
        /// </summary>
        public string PlanGuid { get; set; }

        /// <summary>
        /// Specifies the ID of a service plan in the Plesk components (f.e, Business Manager). 
        /// </summary>
        public string PlanExternalId { get; set; }

    }
}
