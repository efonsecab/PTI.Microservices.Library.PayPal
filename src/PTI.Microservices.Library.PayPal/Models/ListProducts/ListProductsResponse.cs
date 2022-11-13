using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Models.PaypalService.ListProducts
{


    /// <summary>
    /// 
    /// </summary>
    public class ListProductsResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public Product[] products { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total_items { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total_pages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Link1[] links { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime create_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Link[] links { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Link
    {
        /// <summary>
        /// 
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string method { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Link1
    {
        /// <summary>
        /// 
        /// </summary>
        public string href { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string method { get; set; }
    }

}
