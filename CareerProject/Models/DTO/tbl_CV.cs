namespace CareerProject.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_CV
    {
        public long ID { get; set; }

        public long IDUser { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        [Column(TypeName = "ntext")]
        public string FileCV { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
