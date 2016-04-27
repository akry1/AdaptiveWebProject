namespace AdaptiveWebProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Posts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PostId { get; set; }

        public string Data{ get; set; }

        [ForeignKey("PostId")]
        public virtual IList<PostTags> PostTags { get; set; }

        [ForeignKey("PostId")]
        public virtual IList<PostDetails> PostDetails { get; set; }

    }
}
