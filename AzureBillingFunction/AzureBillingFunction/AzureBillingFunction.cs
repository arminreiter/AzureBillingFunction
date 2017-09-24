using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System;
using CodeHollow.AzureBillingApi;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AzureBillingFunction
{
    /// <summary>
    /// Time triggered function that creates a weekly billing report and sends it via mail
    /// </summary>
    public static class AzureBillingFunction
    {
        internal const string CURRENCY_CHAR = "&euro;";
        const string TRIGGER = "0 0 9 * * 1";
        //const string TRIGGER = "*/10 * * * * *";

        [FunctionName("AzureBillingFunction")]
        public static void Run([TimerTrigger(TRIGGER)]TimerInfo myTimer, TraceWriter log, ExecutionContext context)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                var c = new Client(Config.Tenant, Config.ClientId, Config.ClientSecret,
                    Config.SubscriptionId, Config.RedirectUrl);
                
                var path = System.IO.Path.Combine(context.FunctionAppDirectory, "MailReport.html");
                string html = BillingReportGenerator.GetHtmlReport(c, path);

                SendMail(Config.To, Config.ToName, html);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        private static void SendMail(string toMail, string toName, string html)
        {
            var client = new SendGridClient(Config.ApiKey);
            var from = new EmailAddress(Config.From, Config.FromName);
            var to = new EmailAddress(toMail, toName);

            var msg = MailHelper.CreateSingleEmail(from, to, "Weekly Azure Billing Report", "", html);
            client.SendEmailAsync(msg).Wait();
        }
    }
}
