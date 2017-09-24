using CodeHollow.AzureBillingApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBillingFunction
{
    public static class BillingReportGenerator
    {
        public static string GetHtmlReport(Client c, string htmlFile)
        {
            var startdate = DateTime.Now.AddDays(-7); // set start date to last monday
            var enddate = DateTime.Now.AddDays(-1); // set end date to last day which is sunday
            
            var allcosts = c.GetResourceCosts("MS-AZR-0003P", "EUR", "en-US", "AT", startdate, enddate,
            CodeHollow.AzureBillingApi.Usage.AggregationGranularity.Daily, true);

            var costsByResourceGroup = allcosts.GetCostsByResourceGroup();
            
            string cbr = CostsByResourceGroup(costsByResourceGroup);
            string costDetails = CostsDetails(costsByResourceGroup);
                        
            string html = System.IO.File.ReadAllText(htmlFile);
            html = html.Replace("{costsPerResourceGroup}", cbr);
            html = html.Replace("{costsDetails}", costDetails);
            html = html.Replace("{date}", startdate.ToShortDateString() + " - " + enddate.ToShortDateString());

            return html.ToString();
        }

        private static string CostsDetails(Dictionary<string, IEnumerable<ResourceCosts>> costsByResourceGroup)
        {
            var costs = from cbrg in costsByResourceGroup
                        from costsByResourceName in cbrg.Value.GetCostsByResourceName()
                        from costsByMeterName in costsByResourceName.Value.GetCostsByMeterName()
                        select new
                        {
                            ResourceGroup = cbrg.Key,
                            Resource = costsByResourceName.Key,
                            MeterName = costsByMeterName.Key,
                            Costs = costsByMeterName.Value
                        };

            var data = costs.Select(x => $"<tr><td>{x.ResourceGroup}</td><td>{x.Resource}</td><td>{x.MeterName}</td><td>{x.Costs.GetTotalUsage().ToHtml()}</td><td>{x.Costs.GetTotalCosts().ToHtml(true)}</td></tr>");
            return string.Concat(data);
        }

        private static string CostsByResourceGroup(Dictionary<string, IEnumerable<ResourceCosts>> costsByResourceGroup)
        {
            var data = costsByResourceGroup.Select(x => $"<tr><td>{x.Key}</td><td>{x.Value.GetTotalCosts().ToHtml(true)}</td></tr>");
            return string.Concat(data);
        }
    }
}
