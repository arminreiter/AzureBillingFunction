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

        const string FROM = "billingapi@codehollow.com";
        const string FROM_NAME = "Azure Billing API";
        const string TO = "[MAIL]";
        const string TO_NAME = "[NAME]";
        const string APIKEY = "[SENDGRIDAPIKEY]";

        [FunctionName("AzureBillingFunction")]
        public static void Run([TimerTrigger(TRIGGER)]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
            try
            {
                Client c = new Client("mytenant.onmicrosoft.com", "[CLIENTID]",
                    "[CLIENTSECRET]", "[SUBSCRIPTIONID]", "http://[REDIRECTURL]");

                string html = BillingReportGenerator.GetHtmlReport(c, "MailReport.html");

                SendMail(TO, TO_NAME, html);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        private static void SendMail(string toMail, string toName, string html)
        {
            var client = new SendGridClient(APIKEY);
            var from = new EmailAddress(FROM, FROM_NAME);
            var to = new EmailAddress(toMail, toName);

            var msg = MailHelper.CreateSingleEmail(from, to, "Weekly Azure Billing Report", "", html);
            client.SendEmailAsync(msg).Wait();
        }
    }
}
