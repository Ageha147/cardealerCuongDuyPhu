using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanXe.Models;

namespace WebBanXe.Models
{
    public class giohang
    {
        dbBanXeLINQDataContext data = new dbBanXeLINQDataContext();

        public int tMaHang { get; set; }
        public string tTenHang { get; set; }
        public string tAnhBia { get; set; }
        public double tDonGia { get; set; }
        public int tSoLuong { get; set; }
        public double tThanhTien { get { return tSoLuong * tDonGia; } }

        public giohang(int MaHang)
        {
            tMaHang = MaHang;
            MATHANG hang = data.MATHANGs.Single(n => n.MaHang == tMaHang);
            tTenHang = hang.TenHang;
            tAnhBia = hang.AnhBia;
            tDonGia = double.Parse(hang.Gia.ToString());
            tSoLuong = 1;
        }
    }
}