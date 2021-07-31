using System;
using System.ServiceModel.Description;
using Calculation_OpportunityProduct_DeliveryCost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private APP _appService;
        private OrganizationServiceProxy _orgService;
        public UnitTest1()
        {
            var CrmDomain = "afv";
            var CrmPassword = "@Ar1j44";
            var CrmServiceUrl = "http://192.168.239.3/afv/XRMServices/2011/Organization.svc";
            var CrmUsername = "crmadmin";


            ClientCredentials credentials = new System.ServiceModel.Description.ClientCredentials();
            credentials.Windows.ClientCredential = new System.Net.NetworkCredential(CrmUsername, CrmPassword, CrmDomain);

            _orgService = new OrganizationServiceProxy(new Uri(CrmServiceUrl), null, credentials, null);
            _orgService.Timeout = new TimeSpan(0, 2, 0);

            _appService = new APP(_orgService);
        }

        [TestMethod]
        public void TestMethod1()
        {
            EntityReference opportunity = new EntityReference("opportunity", new Guid("5004166D-33B2-EB11-8B7F-000C293836F1"));




            _appService.MainFunction(opportunity);





        }
    }
}
