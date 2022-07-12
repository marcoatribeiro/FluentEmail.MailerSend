using System;
using System.Collections.Generic;

namespace FluentEmail.MailerSend
{
    public sealed class MailerSendOptions
    {
        public string? TemplateId { get; set; }
        public bool? PrecedenceBulk { get; set; }
        public IEnumerable<Variable>? Variables { get; set; }
        public IEnumerable<Personalization>? Personalization { get; set; }
        public DateTime? SendAt { get; set; }
    }
}
