using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.PayPal.Models.CreateBatchPayout
{

    public class CreateBatchPayoutResponse
    {
        public CreateBatchPayoutResponse_Batch_Header batch_header { get; set; }
    }

    public class CreateBatchPayoutResponse_Batch_Header
    {
        public CreateBatchPayoutResponse_Sender_Batch_Header sender_batch_header { get; set; }
        public string payout_batch_id { get; set; }
        public string batch_status { get; set; }
    }

    public class CreateBatchPayoutResponse_Sender_Batch_Header
    {
        public string sender_batch_id { get; set; }
        public string email_subject { get; set; }
        public string email_message { get; set; }
    }

}
