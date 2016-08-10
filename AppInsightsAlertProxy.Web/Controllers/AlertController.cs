using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using AppInsightsAlertProxy.Web.Business.ApplicationInsightsWebHook;
using AppInsightsAlertProxy.Web.Business.ApplicationInsightsWebHook.Models;
using AppInsightsAlertProxy.Web.Business.Slack;
using AppInsightsAlertProxy.Web.ViewModels.Alert;
using Newtonsoft.Json;

namespace AppInsightsAlertProxy.Web.Controllers
{
    public class AlertController : Controller
    {
        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var baseUrl = Request.Url.OriginalString.Replace(Url.Action("Index"), "");
                model.Url = string.Format("{0}{1}", baseUrl, Url.Action("WebHook", new { channel = model.Channel, name = model.Name }));
            }
            
            return View(model);
        }

        [HttpPost]
        public async Task WebHook(string name, string channel)
        {
            if (name == null || channel == null)
                return;

            Request.InputStream.Seek(0, SeekOrigin.Begin);
            using (var sr = new StreamReader(Request.InputStream))
            {
                var json = sr.ReadToEnd();
                var payload = JsonConvert.DeserializeObject<AlertPayload>(json);

                if (payload.Status == "Activated" && payload.Context.ConditionType == "Webtest")
                    await AlertActivatedWebtest(name, channel, payload);
                else if (payload.Status == "Activated" && payload.Context.ConditionType == "ProactiveDiagnostics")
                    await AlertActivatedProactiveDiagnostics(name, channel, payload);
                else if (payload.Status == "Resolved")
                    await AlertResolved(name, channel, payload);
                else
                    await UnknownAlertActivated(name, channel, payload, json);
            }
        }

        private Task AlertActivatedWebtest(string name, string channel, AlertPayload payload)
        {
            var webhookUrl = ConfigurationManager.AppSettings["AlertProxy.SlackWebHook"];
            var failureDetails = AppInsightsHelper.ParseFailureDetails(payload.Context.Condition.FailureDetails);
            var text = string.Format("@channel: Har problemer med nettsiden. <https://portal.azure.com/#resource{0}|Mer informasjon>\r\n```{1}```", payload.Context.ResourceId, failureDetails);
            var fallback = "Har problemer med nettsiden.";
            return SlackHelper.Post(webhookUrl, channel, name, text, fallback, "#ff0000");
        }

        private Task AlertActivatedProactiveDiagnostics(string name, string channel, AlertPayload payload)
        {
            var webhookUrl = ConfigurationManager.AppSettings["AlertProxy.SlackWebHook"];
            var text = string.Format("@channel: Har problemer med nettsiden. <https://portal.azure.com/#resource{0}|Mer informasjon>\r\n```{1}```", payload.Context.ResourceId, payload.Context.Description);
            var fallback = "Har problemer med nettsiden.";
            return SlackHelper.Post(webhookUrl, channel, name, text, fallback, "#ff0000");
        }

        private Task UnknownAlertActivated(string name, string channel, AlertPayload payload, string json)
        {
            var webhookUrl = ConfigurationManager.AppSettings["AlertProxy.SlackWebHook"];
            var text = string.Format("@channel: Ukjent problem med nettsiden. <https://portal.azure.com/#resource{0}|Mer informasjon>\r\n```{1}```", payload.Context.ResourceId, json);
            var fallback = "Ukjent problem med nettsiden.";
            return SlackHelper.Post(webhookUrl, channel, name, text, fallback, "#ff0000");
        }

        private Task AlertResolved(string name, string channel, AlertPayload payload)
        {
            var webhookUrl = ConfigurationManager.AppSettings["AlertProxy.SlackWebHook"];
            var text = string.Format("Problemene ser ut til å være løst. <https://portal.azure.com/#resource{0}|Mer informasjon>", payload.Context.ResourceId);
            var fallback = "Problemene ser ut til å være løst.";
            return SlackHelper.Post(webhookUrl, channel, name, text, fallback, "#00ff00");
        }
    }
}