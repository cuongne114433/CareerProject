namespace CareerProject.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Job
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Job()
        {
            tbl_ApplyJob = new HashSet<tbl_ApplyJob>();
        }

        public long ID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public long IDCompany { get; set; }

        [Column(TypeName = "ntext")]
        public string Requirement { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Benefit { get; set; }

        public double? Offer { get; set; }

        [StringLength(200)]
        public string Industry { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LimitDate { get; set; }

        public int? Total { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        [StringLength(20)]
        public string Sex { get; set; }

        [StringLength(250)]
        public string Location { get; set; }

        public long IDCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ApplyJob> tbl_ApplyJob { get; set; }

        public virtual tbl_Category tbl_Category { get; set; }

        public virtual tbl_Company tbl_Company { get; set; }
    }
}
