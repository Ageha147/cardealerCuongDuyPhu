using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanXe.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanXe.Controllers
{
    public class XeController : Controller
    {
        // GET: Xe

        dbBanXeLINQDataContext data = new dbBanXeLINQDataContext();
        
        private List<MATHANG> laysanpham(int count)
        {
            return data.MATHANGs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult Index(int ? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var sanphammoi = laysanpham(100);
            return View(sanphammoi.ToPagedList(pageNum,pageSize));
        }

        public ActionResult ToanBo(int ? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var sanphammoi = laysanpham(100);
            return View(sanphammoi.ToPagedList(pageNum, pageSize));
        }

        public ActionResult SlidePartial()
        {
            var sanphammoi = laysanpham(5);
            return PartialView(sanphammoi);
        }

        public ActionResult LoaiSP()
        {
            var sanpham = from lh in data.LOAIHANGs select lh;
            return PartialView(sanpham);
        }

        public ActionResult LoaiBody()
        {
            var sanpham = from lb in data.LOAITHANs select lb;
            return PartialView(sanpham);
        }

        public ActionResult SPTheoLoaiHang(int id, int ? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var sanpham = from x in data.MATHANGs where x.MaLoai == id select x;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }

        public ActionResult SPTheoLoaiBody(int id, int ? page)
        {
            int pageSize = 9;
            int pageNum = (page ?? 1);
            var sanpham = from x in data.MATHANGs where x.MaThan == id select x;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Details(int id)
        {
            var sanpham = from x in data.MATHANGs where x.MaHang == id select x;
            return View(sanpham.Single());
        }

        [HttpPost]
        public ActionResult Submit(FormCollection fc)
        {
            TempData["Message"] = "TenLoai: " + fc["TenLoai"];
            TempData["Message"] = "\\nMaLoai" + fc["MaLoai"];
            return RedirectToAction("QuickSearch");
        }

        public ActionResult QuickSearch()
        {
            var search = from q in data.MATHANGs select q;
            return PartialView(search);
        }

        public ActionResult SPTimKiem(int lh)
        {
            var search = from x in data.LOAIHANGs where x.MaLoai == lh select x;
            return View(search);
        }

        public ActionResult QuickSearch2(string lh, string bh)
        {
            var search = from x in data.MATHANGs select x;
            if(!string.IsNullOrEmpty(lh))
            {
                search = search.Where(x => x.TenHang.Contains(lh));
            }
            if(!string.IsNullOrEmpty(bh))
            {
                search = search.Where(x => x.ThongTinBaoHanh.Contains(bh));
            }
            return PartialView(search);
        }
    }   
}