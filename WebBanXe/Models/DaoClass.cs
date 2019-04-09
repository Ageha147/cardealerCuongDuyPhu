using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanXe.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebBanXe.Models
{
    public class DaoClass
    {

        public void SaveHang(hanghoa hh)
        {
            using (SqlConnection con = new SqlConnection("Data Source=GLYWUL-PC;Initial Catalog=QuanLyXe;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand("suaHang", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paraMa = new SqlParameter();
                paraMa.ParameterName = "@MaHang";
                paraMa.Value = hh.sMaHang;
                cmd.Parameters.Add(paraMa);

                SqlParameter paraTen = new SqlParameter();
                paraMa.ParameterName = "@TenHang";
                paraMa.Value = hh.sTenHang;
                cmd.Parameters.Add(paraTen);

                SqlParameter paraNSX = new SqlParameter();
                paraMa.ParameterName = "@NhaSanXuat";
                paraMa.Value = hh.sNhaSanXuat;
                cmd.Parameters.Add(paraNSX);

                SqlParameter paraAnh = new SqlParameter();
                paraMa.ParameterName = "@AnhBia";
                paraMa.Value = hh.sAnhBia;
                cmd.Parameters.Add(paraAnh);

                SqlParameter paraSL = new SqlParameter();
                paraMa.ParameterName = "@SoLuong";
                paraMa.Value = hh.sSoLuong;
                cmd.Parameters.Add(paraSL);

                SqlParameter paraTT = new SqlParameter();
                paraMa.ParameterName = "@ThongTinBaoHanh";
                paraMa.Value = hh.sThongTinBaoHanh;
                cmd.Parameters.Add(paraTT);

                SqlParameter paraNgay = new SqlParameter();
                paraMa.ParameterName = "@NgayCapNhat";
                paraMa.Value = hh.sNgayCapNhat;
                cmd.Parameters.Add(paraNgay);

                SqlParameter paraLoai = new SqlParameter();
                paraMa.ParameterName = "@MaLoai";
                paraMa.Value = hh.sMaLoai;
                cmd.Parameters.Add(paraLoai);

                SqlParameter paraThan = new SqlParameter();
                paraMa.ParameterName = "@MaThan";
                paraMa.Value = hh.sMaThan;
                cmd.Parameters.Add(paraThan);

                SqlParameter paraDV = new SqlParameter();
                paraMa.ParameterName = "@DonViTinh";
                paraMa.Value = hh.sDonViTinh;
                cmd.Parameters.Add(paraDV);

                SqlParameter paraGia = new SqlParameter();
                paraMa.ParameterName = "@Gia";
                paraMa.Value = hh.sGia;
                cmd.Parameters.Add(paraGia);

                SqlParameter paraMT = new SqlParameter();
                paraMa.ParameterName = "@MoTa";
                paraMa.Value = hh.sMoTa;
                cmd.Parameters.Add(paraMT);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}