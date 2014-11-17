using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VsoRestApiNetWrapper
{
    public class Account
    {
        public Guid Id { get; internal set; }

        public string Uri { get; internal set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public AccountStatus Status { get; set; }

        public Guid CreatorId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid LastUpdaterId { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string OrganizationName { get; set; }
    }
}
