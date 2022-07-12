using System.Collections.Generic;

namespace FluentEmail.MailerSend
{
    // According to fields specified in https://developers.mailersend.com/api/v1/email.html
    public class EmailRequest
    {
        public Recipient? From { get; set; }
        public IList<Recipient> To { get; set; } = new List<Recipient>();
        public IList<Recipient>? Cc { get; set; }
        public IList<Recipient>? Bcc { get; set; }
        public IList<Recipient>? ReplyTo { get; set; }
        public string? Subject { get; set; }
        public string? Text { get; set; }
        public string? Html { get; set; }
        public IList<Attachment>? Attachments { get; set; }
        public string? TemplateId { get; set; }
        public IList<string>? Tags { get; set; }
        public IList<Variable>? Variables { get; set; }
        public IList<Personalization>? Personalization { get; set; }
        public bool? PrecedenceBulk { get; set; }
        public int? SendAt { get; set; }
    }
}
