using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AdaptiveWebProject.Models
{
    public class UserTags
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [StringLength(128)]
        public string UserId { get; set; }

        public string Tags { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser AspNetUsers { get; set; }

    }
}