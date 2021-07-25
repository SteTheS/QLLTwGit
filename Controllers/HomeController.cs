using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLlaptop.Models;
using PagedList;
using PagedList.Mvc;

namespace QLlaptop.Controllers
{
    public class HomeController : Controller
    {
        dbQLLTDataContext data = new dbQLLTDataContext();

        private List<SanPham> GetNewProc(int count)
        {
            return data.SanPhams.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        //Xuat list loai
        private List<LoaiSanPham> GetLoai()
        {
            return data.LoaiSanPhams.OrderBy(a => a.Maloai).ToList();
        }

        public PartialViewResult getloai()
        {
            var loai = GetLoai();
            return PartialView(loai);
        }
        
        //Xuat list Brand
        private List<ThuongHieu> GetBrand(int id)
        {
            return data.ThuongHieus.OrderBy(a => a.Mathuonghieu == id).ToList();
        }

        public PartialViewResult getbr(int id)
        {
            var br = GetBrand(id);
            return PartialView(br);
        }

        //Lay 5 laptop gaming moi nhat
        //private List<SanPham> GetLaptopGaming(int count)
        //{
        //    SanPham sp = new SanPham();
        //    return data.SanPhams.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        //}
        //public ActionResult GetLaptopGaming()
        //{
        //    var gaming = from sp in data.SanPhams
        //                 where sp.Maloai == 2
        //                 select sp;
        //    var NewProc = gaming.
        //}

        public ActionResult Index(/*int ? page*/ )
        {
            //int pageSize = 5;
            //int pageNum = (page ?? 1);
            ////Lay 10 laptop moi nhat
            var NewProc = GetNewProc(10);
            return View(NewProc);
        }

        public ActionResult Detail(int id)
        {
            var sanpham = from sp in data.SanPhams
                          where sp.MaSP == id
                          select sp;
            return View(sanpham.Single());
        }

        //Lấy San pham theo loại
        public ActionResult SPtheoloai(int id, int ? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var sanpham = from sp in data.SanPhams where sp.Maloai == id select sp;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }
        //public ActionResult SPtheoloai(int? page)
        //{
        //    int pageNumber = (page ?? 1);
        //    int pageSize = 7;
        //    return View(data.SanPhams.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        //}

        //Lấy Sản phẩm theo thương hiệu
        public ActionResult SPtheothuonghieu(int id, int ? page)
        {
            int pageSize = 12;
            int pageNum = (page ?? 1);
            var sanpham = from sp in data.SanPhams where sp.Mathuonghieu == id select sp;
            return View(sanpham.ToPagedList(pageNum, pageSize));
        }

        //Chăm sóc khách hàng
        public ActionResult Chamsocbanhang()
        {
            return View();
        }

        //Chăm sóc giao nhận
        public ActionResult Chamsocgiaonhan()
        {
            return View();
        }

        //Liên hệ
        public ActionResult Lienhe()
        {
            return View();
        }
    }
}