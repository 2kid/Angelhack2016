using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelHack2016.Models
{
    //public class Feedback
    //{
    //    public int FeedbackId { get; set; }
    //    public string Value { get; set; }
    //}

    //public class FeedbackDetail : Feedback
    //{
    //    public bool IsComplaint { get; set; }
    //    public bool IsProduct { get; set; }
    //}

    //public class FeedbackDescription
    //{
    //    public int FeedbackDescriptionId { get; set; }
    //    public string Feedback { get; set; }

    //    public int FeedbackDetailId { get; set; }
    //    public virtual FeedbackDetail FeedbackDetail { get; set; }
    //}

    public class FeedbackResult
    {
        public int FeedbackResultId  { get; set; }
        public float PositiveSentiments { get; set; }
        public float NegativeSentiments { get; set; }
        public float NeutralSentiments { get; set; }
        public List<Feedback> Feedback { get; set; }

    }

    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Value { get; set; }
        public int transactionNo { get; set; }

        public decimal? SentimentScore { get; set; }

        public int BusinessId { get; set; }
        public virtual Business Business { get; set; }
    }
}