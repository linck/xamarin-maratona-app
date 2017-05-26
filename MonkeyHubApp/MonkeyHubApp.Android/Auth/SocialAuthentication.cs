using System;
using System.Collections.Generic;
using MonkeyHubApp.Auth;
using MonkeyHubApp.Helpers;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using MonkeyHubApp.Droid.Auth;

[assembly: Xamarin.Forms.Dependency(typeof(SocialAuthentication))]
namespace MonkeyHubApp.Droid.Auth
{
    public class SocialAuthentication : IAuthentication
    {
        public async Task<MobileServiceUser> LoginAsybc(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                //TODO: LogError
                return null;
            }
        }
    }
}