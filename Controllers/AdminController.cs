using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLlaptop.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace QLlaptop.Controllers
{
    public class AdminController : Controller
    {
        dbQLLTDataContext data = new dbQLLTDataContext();
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
            var tendn = collection["username"];
            var matkhau = collection["pass"];
            Admin ad = data.Admins.SingleOrDefault(n => n.User == tendn && n.Password == matkhau);
            if(ad != null )
            {
                ViewBag.Thongbao = "Chúc mừng ban đăng nhập thành công";
                Session["User"] = ad;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không chính xác";
            }
         return View();
        }
        //==============================================================================================================================================================================
        public ActionResult Sanpham(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.SanPhams.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Themsanpham()
        {
            ViewBag.Mathuonghieu = new SelectList(data.ThuongHieus.ToList().OrderBy(n => n.Tenthuonghieu), "Mathuonghieu", "Tenthuonghieu");
            ViewBag.Maloai = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themsanpham(SanPham sp , HttpPostedFileBase fileupload, HttpPostedFileBase fileupload_1, HttpPostedFileBase fileupload_2, HttpPostedFileBase fileupload_3)
        {
            

            ViewBag.Mathuonghieu = new SelectList(data.ThuongHieus.ToList().OrderBy(n => n.Tenthuonghieu), "Mathuonghieu", "Tenthuonghieu");
            ViewBag.Maloai = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");   
            if(fileupload == null )
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
               if(ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName);
                    var fileName_1 = Path.GetFileName(fileupload_1.FileName);
                    var path_1 = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName_1);
                    var fileName_2 = Path.GetFileName(fileupload_2.FileName);
                    var path_2 = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName_2);
                    var fileName_3 = Path.GetFileName(fileupload_3.FileName);
                    var path_3 = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName_3);
                    if (System.IO.File.Exists(path) || System.IO.File.Exists(path_1) || System.IO.File.Exists(path_2) || System.IO.File.Exists(path_3))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã trùng";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                        fileupload_1.SaveAs(path_1);
                        fileupload_2.SaveAs(path_2);
                        fileupload_3.SaveAs(path_3);
                    }
                    sp.ImageSP = fileName;
                    sp.ImageSP_1 = fileName_1;
                    sp.ImageSP_2 = fileName_2;
                    sp.ImageSP_3 = fileName_3;
                    data.SanPhams.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
            }

            return RedirectToAction("Sanpham");
        }
        public ActionResult Chitietsanpham(int id)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sp.MaSP;
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Xoasanpham(int id)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sp.MaSP;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost,ActionName("Xoasanpham")]
        public ActionResult Xacnhanxoa(int id)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = sp.MaSP;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SanPhams.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("Sanpham");
        }
        [HttpGet]
        public ActionResult Suasanpham(int id)
        {
            SanPham sp = data.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Mathuonghieu = new SelectList(data.ThuongHieus.ToList().OrderBy(n => n.Tenthuonghieu), "Mathuonghieu", "Tenthuonghieu",sp.Mathuonghieu);
            ViewBag.Maloai = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai",sp.Maloai);
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasanpham(SanPham sp , HttpPostedFileBase fileupload, HttpPostedFileBase fileupload_1, HttpPostedFileBase fileupload_2, HttpPostedFileBase fileupload_3)
        {
            ViewBag.Mathuonghieu = new SelectList(data.ThuongHieus.ToList().OrderBy(n => n.Tenthuonghieu), "Mathuonghieu", "Tenthuonghieu");
            ViewBag.Maloai = new SelectList(data.LoaiSanPhams.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {

                    var fileName = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName);
                    var fileName_1 = Path.GetFileName(fileupload_1.FileName);
                    var path_1 = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName_1);
                    var fileName_2 = Path.GetFileName(fileupload_2.FileName);
                    var path_2 = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName_2);
                    var fileName_3 = Path.GetFileName(fileupload_3.FileName);
                    var path_3 = Path.Combine(Server.MapPath("~/Content/images/All/"), fileName_3);
                    if (System.IO.File.Exists(path) || System.IO.File.Exists(path_1) || System.IO.File.Exists(path_2) || System.IO.File.Exists(path_3))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã trùng";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                        fileupload_1.SaveAs(path_1);
                        fileupload_2.SaveAs(path_2);
                        fileupload_3.SaveAs(path_3);
                    }
                    sp.ImageSP = fileName;
                    sp.ImageSP_1 = fileName_1;
                    sp.ImageSP_2 = fileName_2;
                    sp.ImageSP_3 = fileName_3;
                    //UpdateModel(sp);
                    //data.SubmitChanges();   
                    Query qr = new Query();
                    qr.UpdateSanPham(sp);
                }
                return RedirectToAction("Sanpham");
            }
            
        }
//==============================================================================================================================================================================
        public ActionResult Thuonghieu()
        {
            return View(data.ThuongHieus.ToList());
        }
        [HttpGet]
        public ActionResult Themthuonghieu()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themthuonghieu(ThuongHieu th)
        {
            data.ThuongHieus.InsertOnSubmit(th);
            data.SubmitChanges();
            return RedirectToAction("Thuonghieu");
        }
        public ActionResult Chitietthuonghieu(int id)
        {
            ThuongHieu hp = data.ThuongHieus.SingleOrDefault(n => n.Mathuonghieu == id);
            ViewBag.Mathuonghieu = hp.Mathuonghieu;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hp);
        }
        [HttpGet]
        public ActionResult Xoathuonghieu(int id)
        {
            ThuongHieu gv = data.ThuongHieus.SingleOrDefault(n => n.Mathuonghieu == id);
            ViewBag.Mathuonghieu = gv.Mathuonghieu;
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gv);
        }
        [HttpPost, ActionName("Xoathuonghieu")]
        public ActionResult Xacnhanxoath(int id)
        {
            ThuongHieu gv = data.ThuongHieus.SingleOrDefault(n => n.Mathuonghieu == id);
            ViewBag.Mathuonghieu = gv.Mathuonghieu;
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.ThuongHieus.DeleteOnSubmit(gv);
            data.SubmitChanges();
            return RedirectToAction("Thuonghieu");
        }

        [HttpGet]
        public ActionResult Suathuonghieu(int id)
        {
            ThuongHieu thuonghieu = data.ThuongHieus.SingleOrDefault(n => n.Mathuonghieu == id);

            if(thuonghieu == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(thuonghieu);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suathuonghieu(ThuongHieu thuonghieu)
        {
            if (ModelState.IsValid)
            {
                Query qr = new Query();
                qr.UpdateThuongHieu(thuonghieu);
            }
            return RedirectToAction("Thuonghieu");
        }
        //==============================================================================================================================================================================
        public ActionResult Loaisanpham()
        {
            return View(data.LoaiSanPhams.ToList());
        }
        [HttpGet]
        public ActionResult Themloaisanpham()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themloaisanpham(LoaiSanPham th)
        {
            data.LoaiSanPhams.InsertOnSubmit(th);
            data.SubmitChanges();
            return RedirectToAction("Loaisanpham");
        }
        public ActionResult Chitietloaisanpham(int id)
        {
            LoaiSanPham hp = data.LoaiSanPhams.SingleOrDefault(n => n.Maloai == id);
            ViewBag.Maloai = hp.Maloai;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hp);
        }
        [HttpGet]
        public ActionResult Xoaloaisanpham(int id)
        {
            LoaiSanPham gv = data.LoaiSanPhams.SingleOrDefault(n => n.Maloai == id);
            ViewBag.Maloai = gv.Maloai;
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gv);
        }
        [HttpPost, ActionName("Xoaloaisanpham")]
        public ActionResult Xacnhanxoalsp(int id)
        {
            LoaiSanPham gv = data.LoaiSanPhams.SingleOrDefault(n => n.Maloai == id);
            ViewBag.Maloai = gv.Maloai;
            if (gv == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.LoaiSanPhams.DeleteOnSubmit(gv);
            data.SubmitChanges();
            return RedirectToAction("Loaisanpham");
        }

        [HttpGet]
        public ActionResult Sualoaisanpham(int id)
        {
            LoaiSanPham loaisanpham = data.LoaiSanPhams.SingleOrDefault(n => n.Maloai == id);

            if (loaisanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(loaisanpham);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sualoaisanpham(LoaiSanPham loaisanpham)
        {
            if (ModelState.IsValid)
            {
                Query qr = new Query();
                qr.UpdateLoaiSanPham(loaisanpham);
            }
            return RedirectToAction("Loaisanpham");
        }
        //==============================================================================================================================================================================
        public ActionResult TTKhachhang()
        {
            return View(data.Khachhangs.ToList());
        }
        public ActionResult Chitietkhachhang(int id)
        {
            Khachhang hp = data.Khachhangs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = hp.MaKH;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hp);
        }
        [HttpGet]
        public ActionResult XoaKhachhang(int id)
        {
            Khachhang hp = data.Khachhangs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = hp.MaKH;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hp);
        }
        [HttpPost, ActionName("XoaKhachhang")]
        public ActionResult Xacnhanxoakh(int id)
        {
            Khachhang hp = data.Khachhangs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = hp.MaKH;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.Khachhangs.DeleteOnSubmit(hp);
            data.SubmitChanges();
            return RedirectToAction("TTKhachhang");
        }

        [HttpGet]
        public ActionResult Suakhachhang(int id)
        {
            Khachhang khachhang = data.Khachhangs.SingleOrDefault(n => n.MaKH == id);

            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suakhachhang(Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                Query qr = new Query();
                qr.UpdateKhachhang(khachhang);
            }
            return RedirectToAction("TTKhachhang");
        }
        //==============================================================================================================================================================================
        public ActionResult Dondathang()
        {
            return View(data.DonDatHangs.ToList());
        }
        public ActionResult Chitietddh(int id)
        {
            DonDatHang hp = data.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaDH = hp.MaDH;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hp);
        }
        [HttpGet]
        public ActionResult Xoaddh(int id)
        {
            DonDatHang hp = data.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaDH = hp.MaDH;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hp);
        }
        [HttpPost, ActionName("XoaKhachhang")]
        public ActionResult Xacnhanxoaddh(int id)
        {
            DonDatHang hp = data.DonDatHangs.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaDH = hp.MaDH;
            if (hp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.DonDatHangs.DeleteOnSubmit(hp);
            data.SubmitChanges();
            return RedirectToAction("Dondathang");
        }

    }
}