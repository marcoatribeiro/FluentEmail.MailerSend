using FluentEmail.Core.Interfaces;
using FluentEmail.MailerSend;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class FluentEmailMailerSendBuilderExtensions
    {
        public static FluentEmailServicesBuilder AddMailerSendSender(this FluentEmailServicesBuilder builder,
            string apiToken, Action<MailerSendOptions>? options = null)
        {
            builder.Services.TryAdd(ServiceDescriptor.Scoped<ISender>(_ => new MailerSendSender(apiToken, options)));
            return builder;
        }
    }
}
