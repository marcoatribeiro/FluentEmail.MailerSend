using FluentEmail.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace FluentEmail.MailerSend
{
    internal static class ConversionExtensions
    {
        public static IEnumerable<Recipient> ToRecipients(this IList<Address> @this)
        {
            return @this.Select(x => new Recipient { Email = x.EmailAddress, Name = x.Name });
        }

        public static IEnumerable<Attachment> ToAttachments(this IList<Core.Models.Attachment> @this)
        {
            return @this.Select(x => new Attachment { Content = x.ContentType, Filename = x.Filename, Id = x.ContentId });
        }
    }
}
