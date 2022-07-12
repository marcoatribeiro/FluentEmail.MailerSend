# FluentEmail.MailerSend


This library enables you to use [Mailersend](https://www.mailersend.com/) as a sender for [FluentEmail](https://github.com/lukencode/FluentEmail/).

## Getting Started

Install from NuGet

    PM> Install-Package FluentEmail.MailerSend

## Basic Usage

```csharp
Email.DefaultSender = new MailerSendSender("MAILSENDER_API_TOKEN");

var response = await Email
    .From("from@example.com", "Sender Name")
    .To("recipient@example.com")
    .Subject("E-mail subject")
    .Body("Greetings! This message was sent through MailerSend.")
    .SendAsync();
```

## Dependency Injection

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...

    services.AddFluentEmail("from@example.com")
            .AddRazorRenderer()
            .AddMailerSendSender("MAILSENDER_API_TOKEN", options => 
            {
                options.SendAt = DateTime.Now.AddHours(1);
            });

    // ...
}
```

