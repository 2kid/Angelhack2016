using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelHack2016.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionNo { get; set; }
        public int BusinessId { get; set; }
        public virtual Business Business { get; set; }
    }
}