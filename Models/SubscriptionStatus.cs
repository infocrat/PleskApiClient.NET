using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PleskApi.Models
{
    public enum SubscriptionStatus
    {
        Active = 0,
        DisabledByAdministrator = 16,
        DisabledByReseller = 32,
        DisabledByCustomer = 64
    }
}
