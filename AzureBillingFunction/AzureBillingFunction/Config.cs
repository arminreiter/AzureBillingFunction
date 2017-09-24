using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBillingFunction
{
    public static class Config
    {

        public static string From { get { return GetSetting("From"); } }
        public static string FromName { get { return GetSetting("FromName"); } }
        public static string To { get { return GetSetting("To"); } }
        public static string ToName { get { return GetSetting("ToName"); } }
        public static string ApiKey { get { return GetSetting("ApiKey"); } }
        public static string Tenant { get { return GetSetting("Tenant"); } }
        public static string ClientId { get { return GetSetting("ClientId"); } }
        public static string ClientSecret { get { return GetSetting("ClientSecret"); } }
        public static string SubscriptionId { get { return GetSetting("SubscriptionId"); } }
        public static string RedirectUrl { get { return GetSetting("RedirectUrl"); } }

        private static string GetSetting(string name)
        {
            return Environment.GetEnvironmentVariable(name) ?? String.Empty;
        }
    }
}
