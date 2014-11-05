using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VsoRestApiNetWrapper
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public Guid PublicAlias { get; set; }

        public string EmailAddress { get; set; }

        public int CoreRevision { get; set; }

        public DateTime TimeStamp { get; set; }

        public int Revision { get; set; }
    }
}
