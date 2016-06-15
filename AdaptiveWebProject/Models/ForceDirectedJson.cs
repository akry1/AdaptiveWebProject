using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptiveWebProject.Models
{
    public class ForceDirectedJson
    {
        public ForceDirectedJson()
        {
            this.Tags = new string[3];
            this.list1 = new List<ExpertUsers>();
            this.list2 = new List<ExpertUsers>();
            this.list3 = new List<ExpertUsers>();
        }

        public int Postid { get; set; }

        public string[] Tags { get; set; }

        public List<ExpertUsers> list1 { get; set; }

        public List<ExpertUsers> list2 { get; set; }

        public List<ExpertUsers> list3 { get; set; }
    }
}