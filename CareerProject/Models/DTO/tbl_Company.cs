namespace CareerProject.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Company()
        {
            tbl_Job = new HashSet<tbl_Job>();
        }

        public long ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        public string Avt { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string PassWord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Job> tbl_Job { get; set; }
    }
}
