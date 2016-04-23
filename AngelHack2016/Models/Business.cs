using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelHack2016.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public string Username { get; set; }
        public int maxTransactionNumber { get; set; }
        public int minTransactionNumber { get; set; }
        public string referenceNo { get; set; }

        public double longitude { get; set; }
        public double latitude { get; set; }
        public string quicine { get; set; }
    }

    public class lnglat
    {
        public string BusinessName { get; set; }
        public double lng { get; set; }
        public double lat { get; set; }
    }
}