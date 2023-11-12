using System;
using System.Collections.Generic;

namespace EfCoreSamplesApp.Models
{
    public partial class Orders
    {
        public int Id { get; set; }
        public int? Status { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public byte[] Version { get; set; }
    }
}
