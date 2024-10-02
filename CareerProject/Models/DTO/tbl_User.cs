namespace CareerProject.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_User()
        {
            tbl_ApplyJob = new HashSet<tbl_ApplyJob>();
            tbl_CV = new HashSet<tbl_CV>();
        }

        public long ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoB { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Major { get; set; }

        [Column(TypeName = "ntext")]
        public string JobCity { get; set; }

        [Column(TypeName = "ntext")]
        public string ProfileUser { get; set; }

        [Column(TypeName = "ntext")]
        public string Skill { get; set; }

        public double? Expected { get; set; }

        public int? Experiences { get; set; }

        [StringLength(200)]
        public string Position { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string PassWord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_ApplyJob> tbl_ApplyJob { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CV> tbl_CV { get; set; }
    }
}
