# AzureBillingFunction

A description on how to use and setup this function can be found on my blog:
https://codehollow.com/2017/09/weekly-azure-bil…-azure-functions/ 

I just changed one thing which is now different to the blog post. In this github projekt,
I use settings of azure functions. So if you want to use it, you have 2 ways to configure your function:

1. create local.settings.json and deploy it to azure.
This settings file must contain the following values:
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "[STORAGE CONNECTION STRING]",
    "AzureWebJobsDashboard": "",
    "From": "[FROM MAIL ADDRESS]",
    "FromName": "[FROM NAME]",
    "To": "[TO MAIL ADDRESS]",
    "ToName": "[TO NAME]",
    "ApiKey": "[SENDGRID API KEY]",
    "Tenant": "[YOUR TENANT]",
    "ClientId": "[CLIENT ID (AzureAD)]",
    "ClientSecret": "[CLIENT SECRET (AzureAD)]",
    "SubscriptionId": "[SUBSCRIPTION ID]",
    "RedirectUrl": "[REDIRECT URL (AzureAD)]"
  }
}

or 
2.
you add all mentioned setting from above to the app settings of the azure function