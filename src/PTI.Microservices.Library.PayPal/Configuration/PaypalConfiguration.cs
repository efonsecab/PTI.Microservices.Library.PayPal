using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Configuration
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PaypalConfiguration
    {
        public string Endpoint { get; set; } = "https://api.sandbox.paypal.com";//For security reasons we set it to sandbox by default
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
