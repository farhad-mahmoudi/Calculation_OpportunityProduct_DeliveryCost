using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace Calculation_OpportunityProduct_DeliveryCost
{
    public class APP
    {
        private IOrganizationService service;

        public APP(IOrganizationService service)
        {
            this.service = service;
        }

        public void MainFunction(EntityReference opportunityRef)
        {
            Entity opportunityEnt = service.Retrieve(opportunityRef.LogicalName, opportunityRef.Id, new ColumnSet("new_delivery_estimate"));


            decimal weightAll = 0;
            decimal quantity = 0;
            decimal weight = 0;
            decimal price = 0;
            decimal clearanceFee = 0;
            decimal deliveryCost = 0;
            decimal deliveryEstimate = ((Money)opportunityEnt["new_delivery_estimate"]).Value;
            ///پیدا کردن ردیف های فرصت تجاری
             QueryExpression qe = new QueryExpression("opportunityproduct");
            qe.Criteria.AddCondition("opportunityid", ConditionOperator.Equal, opportunityRef.Id);
            qe.ColumnSet = new ColumnSet("quantity", "new_hardware_weight", "new_price", "new_clearence_fee_price");
            EntityCollection opproducts = service.RetrieveMultiple(qe);

            ///محاسبه وزن کل ردیف ها
            foreach (var item in opproducts.Entities)
            {

                if (item.Contains("quantity") && item.Contains("new_hardware_weight"))
                {
                    quantity = (decimal)item["quantity"];
                    weight = (decimal)item["new_hardware_weight"];

                    weightAll = weightAll + quantity * weight;
                }

            }
            ///محاسبه قیمت های محصول فرصت تجاری
            
            foreach (var item in opproducts.Entities)
            {
                if (item.Contains("quantity") && item.Contains("new_hardware_weight") && item.Contains("new_price") && item.Contains("new_clearence_fee_price"))
                {
                    Entity myOpproduct = new Entity(item.LogicalName, item.Id);
                    quantity = (decimal)item["quantity"];
                    weight = (decimal)item["new_hardware_weight"];
                    price = (decimal)item["new_price"];
                    clearanceFee = (decimal)item["new_clearence_fee_price"];

                    //محاسبه هزینه حمل واحد
                    deliveryCost = deliveryEstimate * weight / weightAll;
                    myOpproduct["new_item_delivery_cost"] = deliveryCost;

                    //محاسبه هزینه هر واحد با احتساب هزینه حمل و هزینه ترخیص
                    myOpproduct["priceperunit"] = deliveryCost + price + clearanceFee;
                    //آپدیت محصول یا ردیف فرصت تجاری
                    service.Update(myOpproduct);
                }
            }


        }


    }
}