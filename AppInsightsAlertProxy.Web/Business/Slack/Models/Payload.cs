using System.Collections.Generic;

namespace AppInsightsAlertProxy.Web.Business.Slack.Models
{
    public class Payload
    {
        public Payload()
        {
            Link_names = 1;
        }

        public string Username { get; set; }
        public string Channel { get; set; }
        public int Link_names { get; private set; }
        public List<Attachment> Attachments { get; set; }
    }
}