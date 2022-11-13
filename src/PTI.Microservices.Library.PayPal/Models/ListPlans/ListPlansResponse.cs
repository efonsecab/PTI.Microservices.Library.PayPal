using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Models.PaypalService.ListPlans
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ListPlansResponse
    {
        public Plan[] plans { get; set; }
        public int total_items { get; set; }
        public int total_pages { get; set; }
        public Link1[] links { get; set; }
    }

    public class Plan
    {
        public string id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string usage_type { get; set; }
        public DateTime create_time { get; set; }
        public Link[] links { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string encType { get; set; }
    }

    public class Link1
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
        public string encType { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }

}
