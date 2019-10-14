namespace WritingPlatform.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommentsUsers
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int WriterWorkId { get; set; }

        [Required]
        [StringLength(500)]
        public string CommentsText { get; set; }

        public virtual Users Users { get; set; }

        public virtual WritersWorks WritersWorks { get; set; }
    }
}
