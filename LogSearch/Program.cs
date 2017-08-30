using System.Threading.Tasks;
using Microsoft.Azure.Management.OperationalInsights;
using System.Configuration;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.OperationalInsights.Models;
using System;

namespace LogSearch
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }
        public static async Task MainAsync(string[] args)
        {
           
            var credentials = SdkContext.AzureCredentialsFactory.FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
            var client = new OperationalInsightsManagementClient(credentials);
            client.SubscriptionId = subscriptionId;

            var parameters = new SearchParameters();
            parameters.Query = "*";
            parameters.Top = top;
            var searchResult = await client.Workspaces.GetSearchResultsAsync(resourceGroup, workspeceName, parameters);
            foreach (var result in searchResult.Value)
            {
                Console.WriteLine(result.ToString());
            }

            Console.ReadLine();
        }

        private static string resourceGroup = ConfigurationManager.AppSettings["resourceGroup"];
        private static string subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];
        private static string clientId = ConfigurationManager.AppSettings["clientId"];
        private static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        private static string tenantId = ConfigurationManager.AppSettings["tenantId"];
        private static string workspeceName = "TelemetrySpike";
        private static int top = 10;

    }
    
}
