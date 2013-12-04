using System;
using RadControlsSilverlightClient.TDocs;

namespace RadControlsSilverlightClient
{
    public class MyTreasuryDocsEntities : TreasuryDocsEntities4
    {
        public MyTreasuryDocsEntities()
            : base(new Uri("http://dev-tdocs.ihess.com/TDocs.svc"))
        {
            //localHost: http://localhost:2934/TDocs.svc
            //DEV: http://dev-tdocs.ihess.com/TDocs.svc
            //PROD: http://tdocs.ihess.com/TDocs.svc
        }
    }
}
