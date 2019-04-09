using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Models;

namespace WebBanXe.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        dbBanXeLINQDataContext data = new dbBanXeLINQDataContext();

        public List<giohang> LayGioHang()
        {
            List<giohang> lst = Session["giohang"] as List<giohang>;
            if(lst == null)
            {
                lst = new List<giohang>();
                Session["giohang"] = lst;
            }
            return lst;
        }

        public ActionResult ThemGioHang(int tMaHang, string strURL)
        {
            List<giohang> lst = LayGioHang();
            giohang hang = lst.Find(n => n.tMaHang == tMaHang);
            if(hang == null)
            {
                hang = new giohang(tMaHang);
                lst.Add(hang);
                return Redirect(strURL);
            }
            else
            {
                hang.tSoLuong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int sum = 0;
            List<giohang> lst = Session["giohang"] as List<giohang>;
            if(lst != null)
            {
                sum = lst.Sum(n => n.tSoLuong);
            }
            return sum;
        }

        private double TongTien()
        {
            double sum = 0;
            List<giohang> lst = Session["giohang"] as List<giohang>;
            if (lst != null)
            {
                sum = lst.Sum(n => n.tThanhTien);
            }
            return sum;
        }

        public ActionResult GioHang()
        {
            List<giohang> lst = LayGioHang();
            if(lst.Count == 0)
            {
                return RedirectToAction("Index", "Xe");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lst);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TSL = TongSoLuong();
            ViewBag.TT = TongTien();
            return PartialView();
        }

        public ActionResult XoaGioHang(int tMaHang)
        {
            List<giohang> lst = LayGioHang();
            giohang hang = lst.SingleOrDefault(n => n.tMaHang == tMaHang);
            if (hang != null)
            {
                hang = new giohang(tMaHang);
                lst.RemoveAll(n => n.tMaHang == tMaHang);
                return RedirectToAction("GioHang");
            }
            if(lst.Count == 0)
            {                
                return RedirectToAction("Index","Xe");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int tMaHang, FormCollection collection)
        {
            MATHANG hang = data.MATHANGs.SingleOrDefault(n => n.MaHang == tMaHang);
            if (hang == null)
            {
                Response.StatusCode = 404;
                //RedirectToAction("error404");
            }
            
            List<giohang> lst = LayGioHang();
            giohang gh = lst.SingleOrDefault(n => n.tMaHang == tMaHang);
            if (gh != null)
            {
                gh.tSoLuong = int.Parse(collection["txtSoLuong"].ToString());
            }
            
            return RedirectToAction("GioHang");
        }

        public ActionResult SuaGioHang()
        {
            if(Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Xe");
            }
            List<giohang> lst = LayGioHang();
            return View(lst);
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<giohang> lst = LayGioHang();
            lst.Clear();
            return RedirectToAction("Index", "Xe");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoanKhachHang"] == null || Session["TaiKhoanKhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["giohang"] == null)
            {
                return RedirectToAction("Index", "Xe");
            }

            List<giohang> lst = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lst);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            PHIEUXUAT px = new PHIEUXUAT();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoanKhachHang"];
            List<giohang> gh = LayGioHang();
            px.MaKhachHang = kh.MaKhachHang;
            px.NgayXuat = DateTime.Now;
            var ngaygiao = string.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            px.NgayGiao = DateTime.Parse(ngaygiao);
            px.DaGiaoHang = false;
            px.DaThanhToan = false;
            data.PHIEUXUATs.InsertOnSubmit(px);
            data.SubmitChanges();
            foreach (var item in gh)
            {
                CTPX ctpx = new CTPX();
                ctpx.MaPhieuXuat = px.MaPhieuXuat;
                ctpx.MaHang = item.tMaHang;
                ctpx.SoLuongXuat = item.tSoLuong;
                ctpx.DonGiaXuat = (double)item.tDonGia;
                data.CTPXes.InsertOnSubmit(ctpx);
            }
            data.SubmitChanges();
            Session["giohang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}