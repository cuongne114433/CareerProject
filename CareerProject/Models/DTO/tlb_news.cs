namespace CareerProject.Models.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tlb_news
    {
        [Key]
        public long IDnews { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreationDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Column(TypeName = "ntext")]
        public string Image { get; set; }
    }
}
