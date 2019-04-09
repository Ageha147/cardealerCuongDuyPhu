using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Web.Security;

namespace WebBanXe.Controllers
{
    public class AdminController : Controller
    {
        dbBanXeLINQDataContext db = new dbBanXeLINQDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var taikhoan = collection["username"];
            var matkhau = collection["password"];
            if (string.IsNullOrEmpty(taikhoan))
            {
                ViewData["error1"] = "Phải nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["error2"] = "Phải nhập mật khẩu";
            }
            else
            {
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.UserAdmin == taikhoan && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["userid"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.ThongBao = " Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult Xe(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.MATHANGs.ToList().OrderBy(n => n.MaHang).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult KH(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.KHACHHANGs.ToList().OrderBy(n => n.MaKhachHang).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LH(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.LOAIHANGs.ToList().OrderBy(n => n.MaLoai).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LT(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.LOAITHANs.ToList().OrderBy(n => n.MaThan).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult ThemMoiHang()
        {
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaThan = new SelectList(db.LOAITHANs.ToList().OrderBy(n => n.TenThan), "MaThan", "TenThan");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoiHang(MATHANG xe, HttpPostedFileBase fileupload)
        {
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaThan = new SelectList(db.LOAITHANs.ToList().OrderBy(n => n.TenThan), "MaThan", "TenThan");
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }            
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/temp/"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    xe.AnhBia = filename;
                    db.MATHANGs.InsertOnSubmit(xe);
                    db.SubmitChanges();
                }                
            }
            return RedirectToAction("Xe");
        }

        [HttpGet]
        public ActionResult ThemKH()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemKH(KHACHHANG k)
        {
                if (ModelState.IsValid)
                {
                    db.KHACHHANGs.InsertOnSubmit(k);
                    db.SubmitChanges();
                }            
            return RedirectToAction("KH");
        }

        [HttpGet]
        public ActionResult ThemLH()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemLH(LOAIHANG h)
        {
            if (ModelState.IsValid)
            {
                db.LOAIHANGs.InsertOnSubmit(h);
                db.SubmitChanges();
            }
            return RedirectToAction("LH");
        }

        [HttpGet]
        public ActionResult ThemLT()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemLT(LOAITHAN t)
        {
            if (ModelState.IsValid)
            {
                db.LOAITHANs.InsertOnSubmit(t);
                db.SubmitChanges();
            }
            return RedirectToAction("LT");
        }

        public ActionResult ChiTietHang(int id)
        {
            MATHANG hang = db.MATHANGs.SingleOrDefault(n => n.MaHang == id);
            ViewBag.MaHang = hang.MaHang;
            if(hang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hang);
        }

        [HttpGet]
        public ActionResult XoaHang(int id)
        {
            MATHANG hang = db.MATHANGs.SingleOrDefault(n => n.MaHang == id);
            ViewBag.MaHang = hang.MaHang;
            if(hang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hang);
        }
        
        [HttpPost,ActionName("XoaHang")]
        public ActionResult XacNhanXoa(int id)
        {
            MATHANG hang = db.MATHANGs.SingleOrDefault(n => n.MaHang == id);
            ViewBag.MaHang = hang.MaHang;
            if(hang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.MATHANGs.DeleteOnSubmit(hang);
            db.SubmitChanges();
            return RedirectToAction("Xe");
        }

        [HttpGet]
        public ActionResult XoaKH(int id)
        {
            KHACHHANG k = db.KHACHHANGs.SingleOrDefault(n => n.MaKhachHang == id);
            ViewBag.MaKhachHang = k.MaKhachHang;
            if (k == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(k);
        }

        [HttpPost, ActionName("XoaKH")]
        public ActionResult XacNhanXoaKH(int id)
        {
            KHACHHANG k = db.KHACHHANGs.SingleOrDefault(n => n.MaKhachHang == id);
            ViewBag.MaKhachHang = k.MaKhachHang;
            if (k == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KHACHHANGs.DeleteOnSubmit(k);
            db.SubmitChanges();
            return RedirectToAction("KH");
        }

        [HttpGet]
        public ActionResult XoaLH(int id)
        {
            LOAIHANG h = db.LOAIHANGs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = h.MaLoai;
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(h);
        }

        [HttpPost, ActionName("XoaLH")]
        public ActionResult XacNhanXoaLH(int id)
        {
            LOAIHANG h = db.LOAIHANGs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = h.MaLoai;
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LOAIHANGs.DeleteOnSubmit(h);
            db.SubmitChanges();
            return RedirectToAction("LH");
        }

        [HttpGet]
        public ActionResult XoaLT(int id)
        {
            LOAITHAN t = db.LOAITHANs.SingleOrDefault(n => n.MaThan == id);
            ViewBag.MaThan = t.MaThan;
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(t);
        }

        [HttpPost, ActionName("XoaLT")]
        public ActionResult XacNhanXoaLT(int id)
        {
            LOAITHAN t = db.LOAITHANs.SingleOrDefault(n => n.MaThan == id);
            ViewBag.MaThan = t.MaThan;
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.LOAITHANs.DeleteOnSubmit(t);
            db.SubmitChanges();
            return RedirectToAction("LT");
        }

        /*public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            hanghoa hh = listhh.Find(s => s.sMaHang == id);

            if(hh == null)
            {
                return HttpNotFound();
            }
            return View(hh);
        }*/

        /*[HttpGet]
        public ActionResult SuaHang(int id)
        {
            HangDBHandle sdb = new HangDBHandle();
            MATHANG hang = db.MATHANGs.SingleOrDefault(n => n.MaHang == id);
            //hanghoa hh = sdb.GetHang().SingleOrDefault(n => n.sMaHang == id);
            if (hang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", hang.MaLoai);
            ViewBag.MaThan = new SelectList(db.LOAITHANs.ToList().OrderBy(n => n.TenThan), "MaThan", "TenThan", hang.MaThan);
            //ViewBag.MaLoai = new SelectList(sdb.GetHang().ToList().OrderBy(n => n.sTenLoai), "MaLoai", "TenLoai", hh.sMaLoai);
            //ViewBag.MaThan = new SelectList(sdb.GetHang().ToList().OrderBy(n => n.sTenThan), "MaThan", "TenThan", hh.sMaThan);
            return View(sdb.GetHang().Find(smodel => smodel.sMaHang == id));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaHang(hanghoa smodel, HttpPostedFileBase fileupload, FormCollection collection)
        {
            
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaThan = new SelectList(db.LOAITHANs.ToList().OrderBy(n => n.TenThan), "MaThan", "TenThan");
            ViewBag.MaLoai = new SelectList(sdb.GetHang().ToList().OrderBy(n => n.sTenLoai), "MaLoai", "TenLoai");
            ViewBag.MaLoai = new SelectList(sdb.GetHang().ToList().OrderBy(n => n.sTenLoai), "MaLoai", "TenLoai");
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/temp"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    // TODO: Add update logic here
                    HangDBHandle sdb = new HangDBHandle();
                    smodel.sAnhBia = filename;
                    var ngaycapnhat = string.Format("{0:MM/dd/yyyy}", collection["NCN"]);
                    smodel.sNgayCapNhat = DateTime.Parse(ngaycapnhat);
                    sdb.UpdateHang(smodel);
                    return RedirectToAction("Xe");
                    
                }
            }
            return View();
        }*/

        [HttpGet]
        public ActionResult SuaHang(int id)
        {
            MATHANG hang = db.MATHANGs.SingleOrDefault(n => n.MaHang == id);
            if (hang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", hang.MaLoai);
            ViewBag.MaThan = new SelectList(db.LOAITHANs.ToList().OrderBy(n => n.TenThan), "MaThan", "TenThan", hang.MaThan);
            return View(hang);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaHang(int id, HttpPostedFileBase fileupload, FormCollection collection)
        {
            MATHANG hang = db.MATHANGs.SingleOrDefault(n => n.MaHang == id);
            ViewBag.MaLoai = new SelectList(db.LOAIHANGs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaThan = new SelectList(db.LOAITHANs.ToList().OrderBy(n => n.TenThan), "MaThan", "TenThan");
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                var ngay = string.Format("{0:MM/dd/yyyy}", collection["NCN"]);
                hang.NgayCapNhat = DateTime.Parse(ngay);
                UpdateModel(hang);
                db.SubmitChanges();
                return RedirectToAction("Xe");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/temp"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    // TODO: Add update logic here
                    hang.AnhBia = filename;
                    var ngay = string.Format("{0:MM/dd/yyyy}", collection["NCN"]);
                    hang.NgayCapNhat = DateTime.Parse(ngay);
                    UpdateModel(hang);
                    db.SubmitChanges();
                    return RedirectToAction("Xe");

                }
            }
            return View();
        }

        /*[HttpPost]
        public ActionResult SuaHang(hanghoa hh)
        {
            if(ModelState.IsValid)
            {
                DaoClass daoclass = new DaoClass();
                daoclass.SaveHang(hh);
                return RedirectToAction("Xe");
            }
            return View(hh);
        }*/

        [HttpGet]
        public ActionResult SuaKH(int id)
        {
            KHACHHANG k = db.KHACHHANGs.SingleOrDefault(n => n.MaKhachHang == id);
            if (k == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(k);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaKH(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {

                // TODO: Add update logic here
                KHACHHANG k = db.KHACHHANGs.SingleOrDefault(n => n.MaKhachHang == id);
                var ngay = string.Format("{0:MM/dd/yyyy}", collection["NS"]);
                k.NgaySinh = DateTime.Parse(ngay);
                UpdateModel(k);
                db.SubmitChanges();
                return RedirectToAction("KH");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SuaLH(int id)
        {
            LOAIHANG h = db.LOAIHANGs.SingleOrDefault(n => n.MaLoai == id);
            ViewBag.MaLoai = h.MaLoai;
            if (h == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(h);
        }

        [HttpPost, ActionName("SuaLH")]
        public ActionResult SuaLoaiHang(int id)
        {
            if (ModelState.IsValid)
            {

                // TODO: Add update logic here
                LOAIHANG h = db.LOAIHANGs.SingleOrDefault(n => n.MaLoai == id);
                UpdateModel(h);
                db.SubmitChanges();
                return RedirectToAction("LH");
            }
            return View();
        }

        [HttpGet]
        public ActionResult SuaLT(int id)
        {
            LOAITHAN t = db.LOAITHANs.SingleOrDefault(n => n.MaThan == id);
            ViewBag.MaThan = t.MaThan;
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(t);
        }

        [HttpPost, ActionName("SuaLT")]
        public ActionResult SuaLoaiThan(int id)
        {
            if (ModelState.IsValid)
            {

                // TODO: Add update logic here
                LOAITHAN t = db.LOAITHANs.SingleOrDefault(n => n.MaThan == id);
                UpdateModel(t);
                db.SubmitChanges();
                return RedirectToAction("LT");
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }
    }
}   
 