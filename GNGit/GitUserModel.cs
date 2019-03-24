using System;
using Newtonsoft.Json;

namespace GNGit
{
    public class GitUserModel
    {
        public GitUserModel()
        {
        }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("avatar_url")]
        public Uri avatarUrl { get; set; }

        [JsonProperty("login")]
        public string login { get; set; }

        [JsonProperty("html_url")]
        public string repos_url { get; set; }

        [JsonProperty("company")]
        public string company { get; set; }

        //public Image
    }
}
