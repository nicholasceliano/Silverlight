using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace TDocsDataService
{
    public class TDocs : DataService<TreasuryDocsEntities4>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.SetEntitySetAccessRule("Currencies", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Divisions", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Citizenships", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Banks", EntitySetRights.AllRead
                            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Contacts", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Entities", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Entitlements", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Signers", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AccountTypes", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Accounts", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AccountsView", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AuditAccountsView", EntitySetRights.AllRead
                          | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AuditAccountsSignersEntitlementsView", EntitySetRights.AllRead
              | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AccountsSignersView", EntitySetRights.AllRead
              | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Accounts_Signers_Entitlements", EntitySetRights.All);
            config.SetEntitySetAccessRule("SignerNameView", EntitySetRights.AllRead
             | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("Active", EntitySetRights.AllRead
             | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("SignersView", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AllAccountsSignersView", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("FBARView", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("FBARSummaryView", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("rptActiveSigners", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("rptEPMonthlyAccountActivity", EntitySetRights.AllRead
            | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("BADSInfo", EntitySetRights.AllRead
                | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("DeletedSignersView", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            config.SetEntitySetAccessRule("AccountSignerEntitlementsView", EntitySetRights.AllRead | EntitySetRights.AllWrite);
            //config.SetEntitySetAccessRule("AuditAccounts", EntitySetRights.AllRead
            //    | EntitySetRights.AllWrite);
            config.SetServiceOperationAccessRule("GetEntitlementsByAccountNumber", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("GetPrimaryContactsByBankId", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("RemoveAccountSignerEntitlement", ServiceOperationRights.AllRead);
        //    config.SetServiceOperationAccessRule("UpdateUser", ServiceOperationRights.All);
            config.UseVerboseErrors = true;
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            
        }

        [WebGet]
        public List<AccountSignerEntitlementsView> GetEntitlementsByAccountNumber(string accountNumber)
        {
            return CurrentDataSource.GetEntitlementsByAccountNumber(accountNumber, System.Data.Objects.MergeOption.NoTracking).ToList();
        }

        [WebGet]
        public List<Contacts> GetPrimaryContactsByBankId(int bankId)
        {
            return CurrentDataSource.GetPrimaryContactsByBankId(bankId, System.Data.Objects.MergeOption.NoTracking).ToList();
        }

        [WebGet]
        public int RemoveAccountSignerEntitlement(int accountSignerEntitlementId, string username)
        {
            CurrentDataSource.RemoveAccountSignerEntitlement(accountSignerEntitlementId, username);
            return 0;
        }
       


    }
}
