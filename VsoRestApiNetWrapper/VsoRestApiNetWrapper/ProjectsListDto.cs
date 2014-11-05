using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VsoRestApiNetWrapper
{
    internal class ProjectsListDto
    {
        public int Count { get; set; }

        [JsonProperty("value")]
        public List<ProjectDto> Projects { get; set; }
    }
}
