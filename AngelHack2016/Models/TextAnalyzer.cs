using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngelHack2016.Models
{
 
    public class TextAnalysisObject
    {
        public TextAnalysisObject()
        {
            documents = new List<document>();
        }

        public void AddDocument(document doc)
        {
            documents.Add(doc);
        }

        public List<document> documents { get; set; }

        public class document
        {
            public string id { get; set; }
            public string text { get; set; }
        }
    }

    public class TextAnalysisResponse
    {
        public TextAnalysisResponse()
        {
            documents = new List<document>();
        }

        public string getScore()
        {

            return documents.Average(r => r.SCORE).ToString();

        }

        public List<document> documents { get; set; }

        public class document
        {
            public string id { get; set; }
            private string score { get; set; }
            public decimal SCORE
            {
                get
                {
                    return decimal.Parse(this.score);
                }
                set
                {
                    this.score = value.ToString();
                }
            }
        }
    }



    //public class LanguageIdentifierResponse
    //{
    //    public LanguageIdentifierResponse()
    //    {
    //        documents = new List<document>();
    //    }

    //    public List<document> documents { get; set; }

    //    public class document
    //    {
    //        public string id { get; set; }
    //        public List<detectedLanguages> detectedLanguages { get; set; }
    //    }

    //    public class detectedLanguages
    //    {
    //        public string name { get; set; }
    //        public string iso6391Name { get; set; }
    //        public string score { get; set; }
    //    }
    //}
}