using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptiveWebProject.Models
{
    public class PostWithDifficulty : EasyPost
    {
        public int PostId { get; set; }
        public string Tags { get; set; }
        public string Question { get; set; }
    }
}