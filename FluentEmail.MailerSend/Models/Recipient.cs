using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FluentEmail.MailerSend
{
    public class Recipient
    {
        public string? Email { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public Dictionary<string, string>? Substitutions { get; set; }
    }
}
