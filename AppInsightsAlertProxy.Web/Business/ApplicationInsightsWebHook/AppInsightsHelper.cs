using System.Text.RegularExpressions;

namespace AppInsightsAlertProxy.Web.Business.ApplicationInsightsWebHook
{
    public static class AppInsightsHelper
    {
        // "1 primary requests, 0 dependant requests and 0 conditional rules failed<br/><br/>Http Error (subtype 'Unexpected 404 - NotFound') occured at 07/13/2016 18:54:06 (UTC) for Uri '<a href=\"https://slackproxy-avinor-prod.azurewebsites.net:443/28858fb0-55f0-45e6-a906-fcf1a48e741c/s\" target=\"_blank\">https://slackproxy-avinor-prod.azurewebsites.net:443/28858fb0-55f0-45e6-a906-fcf1a48e741c/s</a>', step #1 with the error ''404 - NotFound' does not match the expected status '200 - OK'.'.<br/><br/>"
        public static string ParseFailureDetails(string input)
        {
            var result = Regex.Replace(input, "<br/>", "\r\n");
            result = Regex.Replace(result, "<.*?>(.*?)<.*?>", "$1");
            return result.Trim();
        }
    }
}