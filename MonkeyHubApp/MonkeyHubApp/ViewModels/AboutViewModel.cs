using MonkeyHubApp.Helpers;
using Version.Plugin;

namespace MonkeyHubApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Versao => "1.0.0.0";
        public string UserInformation { get; set; }

        public AboutViewModel()
        {
            if (Settings.UserId != "")
            {
                UserInformation = Settings.UserId;
            } else
            {
                UserInformation = "Não Logado";
            }
        }
    }
}
