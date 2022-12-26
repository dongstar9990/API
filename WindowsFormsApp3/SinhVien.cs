using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    public class SinhVien
    {
        public int masv { get; set; }
        public string hoten { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        public int malop { get; set; }
        public string anh { get; set; }

        public SinhVien()
        {

        }
        public SinhVien(int masv, string hoten, string diachi, string dienthoai, int malop, string anh)
        {
            this.masv = masv;
            this.hoten = hoten;
            this.diachi = diachi;
            this.dienthoai = dienthoai;
            this.malop = malop;
            this.anh = anh;
        }
    }
}
