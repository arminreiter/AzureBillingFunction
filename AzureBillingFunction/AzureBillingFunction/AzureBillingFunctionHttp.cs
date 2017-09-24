using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http.Headers;
using CodeHollow.AzureBillingApi;

namespace AzureBillingFunction
{
    /// <summary>
    /// Http Triggered function, returns html page with billing data
    /// </summary>
    public static class AzureBillingFunctionHttp
    {
        [FunctionName("AzureBillingFunctionHttp")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log, ExecutionContext context)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var c = new Client(Config.Tenant, Config.ClientId, Config.ClientSecret,
                Config.SubscriptionId, Config.RedirectUrl);

            var path = System.IO.Path.Combine(context.FunctionAppDirectory, "Report.html");
            string html = BillingReportGenerator.GetHtmlReport(c, path);


            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(html);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
