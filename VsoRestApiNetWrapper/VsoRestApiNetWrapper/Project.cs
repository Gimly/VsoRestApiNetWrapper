using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VsoRestApiNetWrapper
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ProjectState State { get; set; }

        public Uri Url { get; set; }
    }
}
