using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation_OpportunityProduct_DeliveryCost
{

    public class Calculation_OpportunityProduct : CodeActivity
    {

        [Input("لطفا فرصت فروش را وارد نمایید")]
        [ReferenceTarget("opportunity")]
        public InArgument<EntityReference> Opportunity { get; set; }



        protected override void Execute(CodeActivityContext executionContext)
        {


            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            EntityReference opportunityRef = Opportunity.Get(executionContext);
            APP myapp = new APP(service);
            myapp.MainFunction(opportunityRef);






            }
    }
}


