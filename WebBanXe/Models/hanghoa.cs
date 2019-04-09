using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanXe.Models;
using System.ComponentModel.DataAnnotations;


namespace WebBanXe.Models
{
    public class hanghoa
    {
        [Display(Name ="sMaHang")]
        public int sMaHang { get; set; }
	    public string sTenHang { get; set; }
        public string sNhaSanXuat { get; set; }
        public string sAnhBia { get; set; }
        public int sSoLuong { get; set; }
        public string sThongTinBaoHanh { get; set; }
        public DateTime? sNgayCapNhat { get; set; }
        public int sMaLoai { get; set; }
        //public string sTenLoai { get; set; }
        public int sMaThan { get; set; }
        //public string sTenThan { get; set; }
        public string sDonViTinh { get; set; }
        public decimal sGia { get; set; }
        public string sMoTa { get; set; }
    }
}