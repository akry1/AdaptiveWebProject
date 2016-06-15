using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace AdaptiveWebProject.Models
{
    public class PostDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }

        public int? ViewCount { get; set; }

        public int? FiveViewCount { get; set; }

        public double? DelayedDays { get; set; }

        public double? Weight { get; set; }

        public string Difficulty { get; set; }

    }
}