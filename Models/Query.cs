using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QLlaptop.Models
{
    public class Query
    {
        private DBConnection db;
        public Query()
        {
            db = new DBConnection();
        }
        public bool UpdateSanPham(SanPham model)
        {
            string sql = "Update SanPham set TenSP= N'" + model.TenSP + "',Mathuonghieu= " + model.Mathuonghieu + ",Maloai= " + model.Maloai + ",ImageSP='" + model.ImageSP + "',ImageSP_1='" + model.ImageSP_1 + "',ImageSP_2='" + model.ImageSP_2 + "',ImageSP_3='" + model.ImageSP_3 + "'," +
                "CPU=N'" + model.CPU + "',RAM=N'" + model.RAM + "',Bonho=N'" + model.Bonho + "',GPU=N'" + model.GPU + "',Manhinh=N'" + model.Manhinh + "',Conggiaotiep=N'" + model.Conggiaotiep + "',Audio=N'" + model.Audio + "',LAN=N'" + model.LAN + "',WIFI=N'" + model.WIFI + "',Bluetooth=N'" + model.Bluetooth + "'" +
                ",Webcam=N'" + model.Webcam + "',HDH=N'" + model.HDH + "',Pin=N'" + model.Pin + "',Trongluong=N'" + model.Trongluong + "',Mausac=N'" + model.Mausac + "',Kichthuoc=N'" + model.Kichthuoc + "',Soluongton= " + model.Soluongton + ",Giatien= " + model.Giatien + ",Ngaycapnhat='" + model.Ngaycapnhat + "' where MaSP = " + model.MaSP;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            var kq = cmd.ExecuteNonQuery();
            con.Close();
            return kq > 0;
        }

        public bool UpdateThuongHieu(ThuongHieu model)
        {
            string sql = "Update ThuongHieu set Tenthuonghieu = '" + model.Tenthuonghieu + "', Gioithieu = N'" + model.Gioithieu +"' Where Mathuonghieu = " + model.Mathuonghieu;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            var kq = cmd.ExecuteNonQuery();
            con.Close();
            return kq > 0;
        }

        public bool UpdateLoaiSanPham(LoaiSanPham model)
        {
            string sql = "Update LoaiSanPham set Tenloai = N'" + model.Tenloai + "', Mota = N'" + model.Mota + "' Where Maloai = " + model.Maloai;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            var kq = cmd.ExecuteNonQuery();
            con.Close();
            return kq > 0;
        }

        public bool UpdateKhachhang(Khachhang model)
        {
            string sql = "Update Khachhang set Hoten = N'" + model.Hoten + "', Password = '" + model.Password + "', Ngaysinh = '" + model.Ngaysinh + "', Diachi = N'" + model.Diachi + "', Gioitinh = " + model.Gioitinh + ", SDT = '" + model.SDT + "', CMND = '" + model.CMND + "', Email = '" + model.Email + "' Where MaKH = " + model.MaKH;
            SqlConnection con = db.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            var kq = cmd.ExecuteNonQuery();
            con.Close();
            return kq > 0;
        }
    }
}