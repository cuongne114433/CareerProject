using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CareerProject.Common
{
    public class UserLogin
    {
        public long UserID { set; get; }
        public string Mail { set; get; }
        public string TenKhachHang { set; get; }
        public string Role { set; get; }
    }
}