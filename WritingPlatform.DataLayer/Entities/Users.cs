namespace WritingPlatform.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            CommentsUsers = new HashSet<CommentsUsers>();
            WritersWorks = new HashSet<WritersWorks>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LoginUser { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailUser { get; set; }

        [Required]
        [StringLength(50)]
        public string PasswordUser { get; set; }

        public int RoleId { get; set; }

        public bool? IsDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentsUsers> CommentsUsers { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WritersWorks> WritersWorks { get; set; }
    }
}
