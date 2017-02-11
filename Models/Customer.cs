using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

using PleskApi;
using PleskApi.BaseModels;

namespace PleskApi
{
    /*(public class CustomerXmlDataType : ModelXmlDataType
    {
        public static readonly CustomerXmlDataType Add = new CustomerXmlDataType((int)ModelXmlDataTypeValues.Add);
        public static readonly CustomerXmlDataType Get = new CustomerXmlDataType((int)ModelXmlDataTypeValues.Get);
        public static readonly CustomerXmlDataType Set = new CustomerXmlDataType((int)ModelXmlDataTypeValues.Set);

        protected CustomerXmlDataType(int internalValue) : base(internalValue) { }
    }*/


    public class Customer :  PleskXmlable
    {
        /// <summary>
        /// Specifies the date when the specified customer account was created.
        /// </summary>
        [PleskXmlLabel("cr_date")]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Specifies the company name.
        /// Optional (0 to 60 characters long).
        /// </summary>
        [PleskXmlLabel("cname")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string CompanyName { get; set; }

        /// <summary>
        /// Specifies the personal name of the customer who owns the customer account. 
        /// Required (1 to 60 characters long).
        /// </summary>
        [PleskXmlLabel("pname")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string PersonalName { get; set; }

        /// <summary>
        /// Specifies the login name of the customer account. 
        /// Required (1 to 60 characters long).
        /// </summary>
        [PleskXmlLabel("login")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Login { get; set; }

        /// <summary>
        /// Specifies the password of the customer account. 
        /// Required (5 to 14 characters long).
        /// </summary>
        [PleskXmlLabel("pssswd")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        public string Password { get; set; }

        /// <summary>
        /// Specifies the current status of the customer account.
        /// Only status values 0 and 16 can be set up.
        /// </summary>
        [PleskXmlLabel("status")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public CustomerStatus? Status { get; set; }

        /// <summary>
        /// Specifies the phone number of the customer account owner.
        /// Optional (0 to 30 characters long).
        /// </summary>
        [PleskXmlLabel("phone")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Phone { get; set; }

        /// <summary>
        /// Specifies the fax number of the customer account owner.
        /// Optional (0 to 30 characters long).
        /// </summary>
        [PleskXmlLabel("fax")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Fax { get; set; }

        /// <summary>
        /// Specifies the email address of the customer account owner.
        /// Optional (0 to 255 characters long).
        /// </summary>
        [PleskXmlLabel("email")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Email { get; set; }

        /// <summary>
        /// Specifies the postal address of the customer account owner.
        /// (0 to 255 characters long).
        /// </summary>
        [PleskXmlLabel("address")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Address { get; set; }

        /// <summary>
        /// Specifies the city of the customer account owner.
        /// Optional (0 to 50 characters long).
        /// </summary>
        [PleskXmlLabel("city")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string City { get; set; }

        /// <summary>
        /// Specifies the US state of the customer account owner (should be specified for US citizens only). 
        /// Optional (0 to 25 characters long).
        /// </summary>
        [PleskXmlLabel("state")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string State { get; set; }

        /// <summary>
        /// Specifies the zip code of the customer account owner (specified for US citizens only). 
        /// Optional (0 to 10 characters long).
        /// </summary>
        [PleskXmlLabel("pcode")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string ZipCode { get; set; }

        /// <summary>
        /// Specifies the 2-character country code of the customer account owner (US for United States, CA for Canada, etc.). 
        /// Optional (2 characters long).
        /// </summary>
        [PleskXmlLabel("country")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Country { get; set; }

        /// <summary>
        /// Specifies the locale used on the customer account. 
        /// Default value: en-US. Note: Use four-letter locale names (RFC 1766).
        /// </summary>
        [PleskXmlLabel("locale")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Locale { get; set; }

        /// <summary>
        /// Specifies the type of the customer account password.
        /// </summary>
        [PleskXmlLabel("password_type")]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public PasswordType? PasswordType { get; set; }

        /// <summary>
        /// Contains the customer GUID. 
        /// </summary>
        [PleskXmlLabel("guid")]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string Guid { get; set; }

        /// <summary>
        /// Specifies the ID of a customer account owner.
        /// </summary>
        [PleskXmlLabel("owner-id")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public int? OwnerId { get; set; }

        /// <summary>
        /// Specifies the login name of a customer account owner. 
        /// If the customer account owner is Plesk Administrator, specify the admin login name.
        /// Note: If the information about owner is omitted, the customer account belongs to the user who issued the request.
        /// </summary>
        [PleskXmlLabel("owner-login")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string OwnerLogin { get; set; }

        [PleskXmlLabel("vendor-guid")]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string VendorGuid { get; set; }

        /// <summary>
        /// specifies a customer GUID in the Plesk components (for example, Business Manager). 
        /// Note: If the information about owner is omitted, the customer account belongs to the user who issued the request.
        /// </summary>
        [PleskXmlLabel("external-id")]
        [PleskXmlDataType(ModelXmlDataType.CustomerAdd)]
        [PleskXmlDataType(ModelXmlDataType.CustomerGet)]
        public string ExternalId { get; set; }

        public List<XElement> ToXml()
        {
            return SerializeProperties();
        }

        public List<XElement> ToXml(ModelXmlDataType dataType)
        {
            return SerializeProperties(dataType);
        }

        public static Customer FromXml(List<XElement> xml)
        {
            Customer customer = new Customer();
            customer.DeserializeProperties(xml, ModelXmlDataType.CustomerGet);
            return customer;
        }
    }
}
