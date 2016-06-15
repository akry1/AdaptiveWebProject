using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptiveWebProject.Models
{
    public class QuestionParts
    {
        public String Body { get; set; }

        public int PostId { get; set; }

        public ForceDirectedJson force { get; set; }

    }
}