using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GNGit
{
    public class RootObject
    {
        public RootObject()
        {
        }

        // {\"total_count\":17,\"incomplete_results\":false,\"items\":
        [JsonProperty("total_count")]
        public int totalCount { get; set; }

        [JsonProperty("items")]
        public List<GitUserModel> GitUsers { get; set; }
    }
}
