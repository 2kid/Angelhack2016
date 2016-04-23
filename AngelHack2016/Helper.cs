using AngelHack2016.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace AngelHack2016
{
    public class Helper
    {
        public static async Task<int> MakeRequest(ApplicationDbContext db, List<Feedback> list)
        {
            //var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

            TextAnalysisObject tempDoc = new TextAnalysisObject();
         
            foreach(var item in list)
            {
                tempDoc.AddDocument(new TextAnalysisObject.document() { id = item.FeedbackId.ToString(), text = item.Value });
            }

            //HttpResponseMessage response;
            string jsonBody = JsonConvert.SerializeObject(tempDoc);
            string data = string.Empty;

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Ocp-Apim-Subscription-Key", WebConfigurationManager.AppSettings["Ocp-Apim-Subscription-Key"]);
                wc.Headers.Add("Content-Type", "application/json");
                data = wc.UploadString(uri, jsonBody);
            }

                TextAnalysisResponse docList = JsonConvert.DeserializeObject<TextAnalysisResponse>(data);

                int positiveComments = 0;
                int negativeComments = 0;
                int neutralComments = 0;

          
                foreach (var item in list)
                {
                    Decimal score = docList.documents.Where(r => r.id == item.FeedbackId.ToString()).Select(u => u.SCORE).SingleOrDefault();

                    Feedback fb = item;
                    fb.SentimentScore = score;
                    db.Entry(fb).State = EntityState.Modified;
                    db.SaveChanges();

                    if (score >= 0.6m)
                    {
                        positiveComments++;
                    }
                    else if (score <= 0.4m)
                    {
                        negativeComments++;
                    }
                    else
                    {
                        neutralComments++;
                    }
                }
                return 0;
        }

    }
}