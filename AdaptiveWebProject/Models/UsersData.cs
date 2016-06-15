using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AdaptiveWebProject.Models
{
    public partial class UsersData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(128)]
        [Key]
        public string UserId { get; set; }

        public int Points { get; set; }

        public int ExpertiseLevel { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser AspNetUsers { get; set; }

    }
}