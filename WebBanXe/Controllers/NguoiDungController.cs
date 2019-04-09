using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Models;
using System.Web.Security;

namespace WebBanXe.Controllers
{
    public class NguoiDungController : Controller
    {
        dbBanXeLINQDataContext db = new dbBanXeLINQDataContext();
        // GET: NguoiDung
        

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["TenKhachHang"];
            var taikhoan = collection["TaiKhoanKhachHang"];
            var matkhau = collection["MatKhauKhachHang"];
            var matkhaulai = collection["MatKhauNhapLai"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            var diachi = collection["DiaChi"];
            var dienthoai = collection["DienThoai"];
            var email = collection["Email"];
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Error1"] = "Vui lòng nhập họ tên bạn";
            }else if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Error2"] = "Vui lòng nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Error3"] = "Vui lòng nhập mật khẩu";
            }
            else if (string.IsNullOrEmpty(matkhaulai))
            {
                ViewData["Error4"] = "Vui lòng nhập lại mật khẩu";
            }
            else if (string.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Error5"] = "Vui lòng nhập ngày sinh";
            }
            else if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Error6"] = "Vui lòng nhập địa chỉ";
            }
            else if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Error7"] = "Vui lòng nhập số điện thoại";
            }
            else if (string.IsNullOrEmpty(matkhaulai))
            {
                ViewData["Error8"] = "Vui lòng nhập email";
            }
            else
            {
                kh.TenKhachHang = hoten;
                kh.TaiKhoanKhachHang = taikhoan;
                kh.MatKhauKhachHang = matkhau;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                kh.DiaChi = diachi;
                kh.DienThoai = dienthoai;
                kh.Email = email;
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var taikhoan = collection["TaiKhoanKhachHang"];
            var matkhau = collection["MatKhauKhachHang"];
            if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Error1"] = "Vui lòng nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Error2"] = "Vui lòng nhập mật khẩu";
            }           
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoanKhachHang == taikhoan && n.MatKhauKhachHang == matkhau);
                if(kh != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoanKhachHang"] = kh;
                    Session["UserName"] = kh.TenKhachHang;
                    return RedirectToAction("Index", "Xe");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return this.DangNhap();
        }

        [HttpGet]
        public ActionResult DangNhapV1()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult DangNhapV1(FormCollection collection)
        {
            var taikhoan = collection["TaiKhoanKhachHang"];
            var matkhau = collection["MatKhauKhachHang"];
            if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["Error1"] = "Vui lòng nhập tài khoản";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Error2"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TaiKhoanKhachHang == taikhoan && n.MatKhauKhachHang == matkhau);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoan"] = kh;

                }
                else
                {
                    return RedirectToAction("DangNhap","NguoiDung");
                }
            }
            return this.DangNhapV1();
        }

        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Xe");
        }
    }
}