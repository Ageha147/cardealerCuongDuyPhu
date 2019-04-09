using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanXe.Models;
using System.ComponentModel.DataAnnotations;

namespace WebBanXe.Models
{
    public class loaihang
    {
        [Display(Name = "sMaLoai")]
        public int sMaLoai { get; set; }
        public string sTenLoai { get; set; }
    }
}