using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PleskApi
{
    [PleskXmlEnumType(true)] // Converts enum to int while serializing to xml
    public enum CustomerStatus
    {
        Active = 0,
        DisabledByAdmin = 16, // can not be set up
        UnderBackupOrRestore = 4, // can not be set up
        Expired = 256 // can not be set up
    }
}
