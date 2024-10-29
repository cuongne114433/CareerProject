using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CareerProject.Models.DTO
{
    public class tbl_ApplyCustom
    {
        public tbl_ApplyJob applyJob { get; set; }
        [Column(TypeName = "ntext")]
        public string FileCV { get; set; }
    }
}