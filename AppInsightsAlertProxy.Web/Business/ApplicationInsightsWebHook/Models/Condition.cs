namespace AppInsightsAlertProxy.Web.Business.ApplicationInsightsWebHook.Models
{
    public class Condition
    {
        public string MetricName { get; set; }
        public string MetricUnit { get; set; }
        public string MetricValue { get; set; }
        public string Threshold { get; set; }
        public string WindowSize { get; set; }
        public string TimeAggregation { get; set; }
        public string Operator { get; set; }

        public string WebTestName { get; set; }
        public string FailureDetails { get; set; }
    }
}