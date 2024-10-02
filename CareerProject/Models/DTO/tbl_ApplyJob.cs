namespace CareerProject.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ApplyJob
    {
        public long ID { get; set; }

        public long IDUser { get; set; }

        public long IDJob { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AppliedDate { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Mail { get; set; }

        [Column(TypeName = "ntext")]
        public string CV { get; set; }

        [Column(TypeName = "ntext")]
        public string CoverLetter { get; set; }

        [StringLength(100)]
        public string ApplyStatus { get; set; }

        public virtual tbl_Job tbl_Job { get; set; }

        public virtual tbl_User tbl_User { get; set; }
    }
}
