using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppInsightsAlertProxy.Web.Business.Slack.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AppInsightsAlertProxy.Web.Business.Slack
{
    public static class SlackHelper
    {
        public static async Task Post(string webhookUrl, string channel, string username, string text, string fallback, string color)
        {
            var payload = new Payload
            {
                Username = username,
                Channel = channel,
                Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Text = text,
                        Fallback = fallback,
                        Color = color
                    }
                }
            };

            var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(webhookUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
    }
}