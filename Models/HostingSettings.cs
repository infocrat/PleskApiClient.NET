using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace PleskApi.Models
{
    /// <summary>
    /// Hosting settings define what type of hosting is provided with a subscription.
    /// </summary>
    public class HostingSettings
    {

    }

    /// <summary>
    /// Hosting settings for virtual hosting.
    /// </summary>
    public class VirtualHostingSettings : HostingSettings
    {
        [Display(ShortName = "ftp_login")]
        public string FtpLogin { get; set; }

        [Display(ShortName = "ftp_password")]
        public string FtpPassword { get; set; }

        /// <summary>
        /// Specify a hosting parameters
        /// </summary>
        //public Dictionary<string, string> Properties { get; set; }

        /// <summary>
        /// Specifies IP addresses associated with a subscription. 
        /// You can provide either one IPAddress node for the subscription's IPv4 or IPv6 address or two such nodes for both of them.
        /// </summary>
        [Required]
        [Display(ShortName = "ip_address")]
        public IPAddress IPAddress { get; set; }

        public VirtualHostingSettings()
        {
            //Properties = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// Hosting settings for standard forwarding.
    /// </summary>
    public class ForwardingHostingSettings : HostingSettings
    {
        /// <summary>
        /// Specifies the URL to which the user will be redirected explicitly at the attempt to visit the specified site.
        /// </summary>
        [Required]
        public string DestinationUrl { get; set; }

        /// <summary>
        /// Specifies IP addresses associated with the site. 
        /// You can provide either one ip_address node for the site's IPv4 or IPv6 address or two such nodes for both of them.
        /// </summary>
        [Required]
        public IPAddress IPAddress { get; set; }
    }

    /// <summary>
    /// No hosting ships with a subscription.
    /// Reserved.
    /// </summary>
    public class NoneHostingSettings : HostingSettings
    {

    }
}
