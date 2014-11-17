using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VsoRestApiNetWrapper
{
    internal class AccountDto
    {
        public Guid AccountId { get; set; }
     
        public string AccountUri { get; set; }
        
        public string AccountName { get; set; }
        
        public string OrganizationName { get; set; }
        
        public Guid AccountOwner { get; set; }
        
        public Guid CreatedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public AccountStatus AccountStatus { get; set; }
        
        public Guid LastUpdatedBy { get; set; }
        
        public DateTime LastUpdatedDate { get; set; }
    }
}
