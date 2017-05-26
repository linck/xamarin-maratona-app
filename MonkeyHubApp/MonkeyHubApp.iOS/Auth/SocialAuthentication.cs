using System;
using System.Collections.Generic;
using MonkeyHubApp.Auth;
using MonkeyHubApp.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using MonkeyHubApp.iOS.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace MonkeyHubApp.iOS.Auth
{
    public class SocialAuthentication: IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsybc(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var current = GetController();
                var user = await client.LoginAsync(current, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception)
            {
                //TODO: LogError
                return null;
            }
        }

        private UIKit.UIViewController GetController()
        {
            var window = UIKit.UIApplication.SharedApplication.KeyWindow;
            var root = window.RootViewController;

            if (root == null) return null;

            var current = root;

            while(current.PresentedViewController != null)
            {
                current = current.PresentedViewController;
            }

            return current;
        }
    }
}