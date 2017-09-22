using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureBillingFunction
{
    public static class Extensions
    {
        public static string ToHtml(this double value, bool currency = false)
        {
            if (currency)
                return value.ToString("0.00") + " " + AzureBillingFunction.CURRENCY_CHAR;
            return value.ToString("0.################");
        }
    }
}
