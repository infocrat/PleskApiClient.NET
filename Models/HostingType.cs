using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EnumAnnotations;

namespace PleskApi.Models
{
    public enum HostingType
    {
        [Display(Name = "vrt_hst")]
        Virtual,

        [Display(Name = "std_fwd")]
        Forwarding,

        [Display(Name = "frm_fwd")]
        FrameForwarding,

        [Display(Name = "none")]
        None
    }
}
