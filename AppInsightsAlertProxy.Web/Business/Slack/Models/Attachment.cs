using System.Collections.Generic;

namespace AppInsightsAlertProxy.Web.Business.Slack.Models
{
    public class Attachment
    {
        public string Text { get; set; }
        public string Fallback { get; set; }
        public string Color { get; set; }
        public List<string> Mrkdwn_in { get; private set; }
        public bool Unfurl_links { get; private set; }

        public Attachment()
        {
            Mrkdwn_in = new List<string> {"text"};
        }
    }
}