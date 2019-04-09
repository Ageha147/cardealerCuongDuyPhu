using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebBanXe.Models
{
    public class khachhang
    {
        [Display(Name = "sMaKhachHang")]
        public int sMaKhachHang { get; set; }
        public string sTenKhachHang { get; set; }
        public string sTaiKhoanKhachHang { get; set; }
        public string sMatKhauKhachHang { get; set; }
        public string sMatKhauNhapLai { get; set; }
        public DateTime? sNgaySinh { get; set; }
        public string sDiaChi { get; set; }
        public string sDienThoai { get; set; }
        public string sEmail { get; set; }
    }
}