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
