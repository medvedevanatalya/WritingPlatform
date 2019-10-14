namespace WritingPlatform.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WritersWorks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WritersWorks()
        {
            CommentsUsers = new HashSet<CommentsUsers>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TitleWork { get; set; }

        [Required]
        public string WriterWorkText { get; set; }

        public int GenreId { get; set; }

        public int UserId { get; set; }

        public DateTime? PublicationDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentsUsers> CommentsUsers { get; set; }

        public virtual Genres Genres { get; set; }

        public virtual Users Users { get; set; }
    }
}
