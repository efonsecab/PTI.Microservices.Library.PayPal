using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.PayPal.Models.GetPayoutBatchDetails
{

    public class GetPayoutBatchDetailsResponse
    {
        public GetPayoutBatchDetailsResponse_Batch_Header batch_header { get; set; }
        public GetPayoutBatchDetailsResponse_Item[] items { get; set; }
        public GetPayoutBatchDetailsResponse_Link[] links { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Batch_Header
    {
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
        public DateTime time_created { get; set; }
        public DateTime time_completed { get; set; }
        public GetPayoutBatchDetailsResponse_Sender_Batch_Header sender_batch_header { get; set; }
        public GetPayoutBatchDetailsResponse_Amount amount { get; set; }
        public GetPayoutBatchDetailsResponse_Fees fees { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Sender_Batch_Header
    {
        public string sender_batch_id { get; set; }
        public string email_subject { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Amount
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Fees
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Item
    {
        public string payout_item_id { get; set; }
        public string transaction_id { get; set; }
        public string transaction_status { get; set; }
        public string payout_batch_id { get; set; }
        public GetPayoutBatchDetailsResponse_Payout_Item_Fee payout_item_fee { get; set; }
        public GetPayoutBatchDetailsResponse_Payout_Item payout_item { get; set; }
        public DateTime time_processed { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Payout_Item_Fee
    {
        public string currency { get; set; }
        public string value { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Payout_Item
    {
        public string recipient_type { get; set; }
        public GetPayoutBatchDetailsResponse_Amount1 amount { get; set; }
        public string note { get; set; }
        public string receiver { get; set; }
        public string sender_item_id { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Amount1
    {
        public string value { get; set; }
        public string currency { get; set; }
    }

    public class GetPayoutBatchDetailsResponse_Link
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string method { get; set; }
    }

}
