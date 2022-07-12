using FluentEmail.Core.Models;
using System.Collections.Generic;

namespace FluentEmail.MailerSend
{
    // According to fields specified in https://developers.mailersend.com/api/v1/email.html
    public class EmailRequest
    {
        public Recipient? From { get; set; }
        public IEnumerable<Recipient> To { get; set; } = new List<Recipient>();
        public IEnumerable<Recipient>? Cc { get; set; }
        public IEnumerable<Recipient>? Bcc { get; set; }
        public IEnumerable<Recipient>? ReplyTo { get; set; }
        public string? Subject { get; set; }
        public string? Text { get; set; }
        public string? Html { get; set; }
        public IEnumerable<Attachment>? Attachments { get; set; }
        public string? TemplateId { get; set; }
        public IEnumerable<string>? Tags { get; set; }
        public IEnumerable<Variable>? Variables { get; set; }
        public IEnumerable<Personalization>? Personalization { get; set; }
        public bool? PrecedenceBulk { get; set; }
        public long? SendAt { get; set; }

        public static EmailRequest FromEmailData(EmailData emailData, MailerSendOptions options) => new EmailRequest
        {
            From = emailData.FromAddress.ToRecipient(),
            To = emailData.ToAddresses.ToRecipients(),
            Cc = emailData.CcAddresses.ToRecipients(),
            Bcc = emailData.BccAddresses.ToRecipients(),
            ReplyTo = emailData.ReplyToAddresses.ToRecipients(),
            Subject = emailData.Subject,
            Text = emailData.IsHtml ? emailData.PlaintextAlternativeBody : emailData.Body,
            Html = emailData.IsHtml ? emailData.Body : null,
            Attachments = emailData.Attachments.ToAttachments(),
            Tags = emailData.Tags,
            TemplateId = options.TemplateId,
            Variables = options.Variables,
            Personalization = options.Personalization,
            PrecedenceBulk = options.PrecedenceBulk,
            SendAt = options.SendAt.ToUnixTime()
        };
    }
}
