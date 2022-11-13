using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.PayPal.Models.CreateBatchPayout
{

    public class CreateBatchPayoutRequest
    {
        public Sender_Batch_Header sender_batch_header { get; set; }
        public Item[] items { get; set; }
    }

    public class Sender_Batch_Header
    {
        public string sender_batch_id { get; set; }
        public string email_subject { get; set; }
        public string email_message { get; set; }
    }

    public class Item
    {
        public string recipient_type { get; set; }
        public Amount amount { get; set; }
        public string note { get; set; }
        public string sender_item_id { get; set; }
        public string receiver { get; set; }
        public Alternate_Notification_Method alternate_notification_method { get; set; }
        public string notification_language { get; set; }
    }

    public class Amount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class Alternate_Notification_Method
    {
        public Phone phone { get; set; }
    }

    public class Phone
    {
        public string country_code { get; set; }
        public string national_number { get; set; }
    }

}
