using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AdaptiveWebProject.Models
{
    public class PostTags
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PostId { get; set; }

        public string Tags { get; set; }

    }
}