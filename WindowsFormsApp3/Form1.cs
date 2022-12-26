using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class tb_maLop : Form
    {
        public tb_maLop()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienThi();
        }

        public void hienThi()
        {
            string link = "http://localhost:100/datavs2/api/th/dssv";
            HttpWebRequest req= HttpWebRequest.CreateHttp(link);
            WebResponse res = req.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SinhVien[]));
            object data = js.ReadObject(res.GetResponseStream());
            SinhVien[] danhsach = (SinhVien[])data;
            dataGridView1.DataSource = danhsach ;
            dsLopToCombobox();
        }
        public void dsLopToCombobox()
        {
            string link = "http://localhost:100/datavs2/api/th/dslop";
            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            WebResponse res = req.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(LopHoc[]));
            object data = js.ReadObject(res.GetResponseStream());
            LopHoc[] danhsach = (LopHoc[])data;
            cbb_lop.DataSource = danhsach;
            cbb_lop.ValueMember = "malop";
            cbb_lop.DisplayMember = "tenlop";

        }

        private void bt_tim_Click(object sender, EventArgs e)
        {
            string str = "?masv=" + tb_maSV.Text;
            string link = "http://localhost:100/datavs2/api/th/timsv" + str;
            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            WebResponse res = req.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SinhVien));
            object data = js.ReadObject(res.GetResponseStream());
            SinhVien sv = (SinhVien)data;
           
            if (sv != null)
            {
                tb_maSV.Text = sv.masv.ToString();
                tb_diaChi.Text = sv.diachi;
                tb_dienThoai.Text = sv.dienthoai;
                //tb_malop.text = sv.malop;
                cbb_lop.SelectedValue = sv.malop;
                tb_anh.Text = sv.anh;
                tb_tenSV.Text = sv.hoten;
            }
            else
            {
                MessageBox.Show("Không có sinh viên có mã " + tb_maSV.Text, "Thông báo ");
            }
        }

        private void bt_danhSach_Click(object sender, EventArgs e)
        {
            hienThi();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            //post
            string url = "http://localhost:100/datavs2/api/th/themsv";
            var client = new WebClient();
            var sv = new NameValueCollection();
            sv["hoten"] = tb_tenSV.Text;
            sv["diachi"] = tb_diaChi.Text;
            sv["dienthoai"] = tb_dienThoai.Text;
            //sv["malop"] = txt_maLop.Text;
            sv["malop"] = cbb_lop.SelectedValue.ToString();

            sv["anh"]=tb_anh.Text;
            var response = client.UploadValues(url,"POST",sv);
            string msg = Encoding.UTF8.GetString(response);
            MessageBox.Show("Kết quả thêm " + msg);
            hienThi();

        }

        private void bt_capNhap_Click(object sender, EventArgs e)
        {
            //post
            string url = "http://localhost:100/datavs2/api/th/suasv";
            var client = new WebClient();
            var sv = new NameValueCollection();
            sv["hoten"] = tb_tenSV.Text;
            sv["diachi"] = tb_diaChi.Text;
            sv["dienthoai"] = tb_dienThoai.Text;
            //sv["malop"] = txt_maLop.Text;
            sv["malop"] = cbb_lop.SelectedValue.ToString();
            sv["anh"] = tb_anh.Text;
            sv["masv"] = tb_maSV.Text;
            var response = client.UploadValues(url, "PUT", sv);
            string msg = Encoding.UTF8.GetString(response);
            MessageBox.Show("Kết quả thêm " + msg);
            hienThi();
        }

        private void bt_Xoa_Click(object sender, EventArgs e)
        {
            string str = "?masv=" + tb_maSV.Text;
            string link = "http://localhost:100/datavs2/api/th/xoasv" + str;
            var client = new WebClient();
            var sv = new NameValueCollection();
            //sv["hoten"] = tb_tenSV.Text;
            //sv["diachi"] = tb_diaChi.Text;
            //sv["dienthoai"] = tb_dienThoai.Text;
            ////sv["malop"] = txt_maLop.Text;
            //sv["malop"] = cbb_lop.SelectedValue.ToString();
            //sv["anh"] = tb_anh.Text;
            //sv["masv"] = tb_maSV.Text;
            var response = client.UploadValues(link, "DELETE", sv);
            string msg = Encoding.UTF8.GetString(response);
            MessageBox.Show("Kết quả thêm " + msg);
            hienThi();
        }
    }
}
