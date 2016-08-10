using System.ComponentModel.DataAnnotations;

namespace AppInsightsAlertProxy.Web.ViewModels.Alert
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Du må angi ett navn. (f.eks avinor.no)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Du må angi en kanal. (f.eks #avinor-no)")]
        [RegularExpression("[@|#]+.+", ErrorMessage = "Kanalen må starte med #, eller @ hvis det er en person.")]
        public string Channel { get; set; }

        public string Url { get; set; }
    }
}