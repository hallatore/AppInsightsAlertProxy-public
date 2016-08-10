using System;

namespace AppInsightsAlertProxy.Web.Business.ApplicationInsightsWebHook.Models
{
    public class Context
    {
        public DateTime Timestamp { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ConditionType { get; set; }
        public Condition Condition { get; set; }
        public string SubscriptionId { get; set; }
        public string ResourceGroupName { get; set; }
        public string ResourceName { get; set; }
        public string ResourceType { get; set; }
        public string ResourceId { get; set; }
        public string ResourceRegion { get; set; }
        public string PortalLink { get; set; }
    }
}