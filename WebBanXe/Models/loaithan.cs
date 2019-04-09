using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanXe.Models;
using System.ComponentModel.DataAnnotations;

namespace WebBanXe.Models
{
    public class loaithan
    {
        [Display(Name = "sMaThan")]
        public int sMaThan { get; set; }
        public string sTenThan { get; set; }
    }
}