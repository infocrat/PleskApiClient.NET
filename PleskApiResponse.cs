using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace PleskApi
{
    public enum PleskApiResponseStatus
    {
        Ok,
        Error
    }
    public enum PleskApiResponseErrorCode
    {
        AccountExists = 1007 // Account already exists
/*            2204
System user setting was failed. Error: The password should be  4 - 255 character
s long and should not contain the username.
Do not use quotes, spaces, and national alphabetic characters in the password.*/
/*Error
2204
Unable to update hosting preferences. Unable to check system user existance: log
in name is empty. Incorrect fields: "login".*/
    }
    public class PleskApiResponse
    {
        public PleskApiResponseStatus Status { get; set; }
        public string ErrorCode { get; set; }
        //public PleskApiResponseErrorCode ErrorCodeCode { get; set; }
        public string ErrorText { get; set; }

        public static PleskApiResponse Parse(XElement message)
        {
            PleskApiResponse result = new PleskApiResponse();

            result.Status = message.Descendants("status").FirstOrDefault().Value.ToLower().Equals("ok") ? PleskApiResponseStatus.Ok : PleskApiResponseStatus.Error;
            result.ErrorCode = message.Descendants("errcode").FirstOrDefault()?.Value;
            result.ErrorText = message.Descendants("errtext").FirstOrDefault()?.Value;

            return result;
        }

        public void ShallowConvert<U>(U child)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(child, property.GetValue(this, null), null);
                }
            }
        }
    }

    public class CustomerAddResponse : PleskApiResponse
    {
        public int? CustomerId { get; set; }
        public string CustomerGuid { get; set; }
        new public static CustomerAddResponse Parse(XElement message)
        {
            CustomerAddResponse result = new CustomerAddResponse();

            result.Status = message.Descendants("status").FirstOrDefault().Value.ToLower().Equals("ok") ? PleskApiResponseStatus.Ok : PleskApiResponseStatus.Error;
            result.ErrorCode = message.Descendants("errcode").FirstOrDefault()?.Value;
            result.ErrorText = message.Descendants("errtext").FirstOrDefault()?.Value;
            try
            {
                result.CustomerId = Convert.ToInt32(message.Descendants("id").FirstOrDefault()?.Value);
            }
            catch { }
            result.CustomerGuid = message.Descendants("guid").FirstOrDefault()?.Value;

            return result;
        }
    }

    public class CustomerGetResponse : PleskApiResponse
    {
        public int CustomerId { get; set; }
        public string CustomerGuid { get; set; }

        public List<Customer> Customers { get; set; }

        new public static CustomerGetResponse Parse(XElement message)
        {
            CustomerGetResponse result = new CustomerGetResponse();

            result.Status = message.Descendants("status").FirstOrDefault().Value.ToLower().Equals("ok") ? PleskApiResponseStatus.Ok : PleskApiResponseStatus.Error;
            result.ErrorCode = message.Descendants("errcode").FirstOrDefault()?.Value;
            result.ErrorText = message.Descendants("errtext").FirstOrDefault()?.Value;

            if (message.Descendants("result").Any())
                result.Customers = message.Descendants("result")
                                            .Select(r => Customer.FromXml(r.Descendants("gen_info").FirstOrDefault().Descendants().ToList()))
                                            .ToList();

            result.CustomerId = Convert.ToInt32(message.Descendants("id").FirstOrDefault()?.Value);
            result.CustomerGuid = message.Descendants("guid").FirstOrDefault()?.Value;

            return result;
        }
    }

    public class SubscriptionAddResponse : PleskApiResponse
    {
        public int? SubscriptionId { get; set; }
        public string SubscriptionGuid { get; set; }

        new public static SubscriptionAddResponse Parse(XElement message)
        {
            SubscriptionAddResponse result = new SubscriptionAddResponse();

            result.Status = message.Descendants("status").FirstOrDefault().Value.ToLower().Equals("ok") ? PleskApiResponseStatus.Ok : PleskApiResponseStatus.Error;
            result.ErrorCode = message.Descendants("errcode").FirstOrDefault()?.Value;
            result.ErrorText = message.Descendants("errtext").FirstOrDefault()?.Value;
            try
            {
                result.SubscriptionId = Convert.ToInt32(message.Descendants("id").FirstOrDefault()?.Value);
            }
            catch { }
            result.SubscriptionGuid = message.Descendants("guid").FirstOrDefault()?.Value;

            return result;
        }
    }

    public class SessionCreateResponse : PleskApiResponse
    {
        public string SessionId { get; set; }

        new public static SessionCreateResponse Parse(XElement message)
        {
            SessionCreateResponse result = new SessionCreateResponse();
            PleskApiResponse.Parse(message).ShallowConvert<SessionCreateResponse>(result);

            result.SessionId = message.Descendants("id").FirstOrDefault()?.Value;
            return result;
        }
    }
}
