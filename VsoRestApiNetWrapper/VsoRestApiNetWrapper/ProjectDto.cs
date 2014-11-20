using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VsoRestApiNetWrapper
{
    internal class ProjectDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Url { get; set; }
        
        public ProjectState State { get; set; }
    }
}
