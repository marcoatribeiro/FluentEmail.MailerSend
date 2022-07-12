using FluentAssertions;
using FluentEmail.Core;

namespace FluentEmail.MailerSend.Tests;

public class MailerSendSenderTests
{
    private readonly string _apiToken = "MAILSENDER_API_TOKEN";

    //[Fact]
    //public async void RealToken()
    //{
    //    var apiToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiYWQ5MDZlY2MyMGY3OTY1ZWI1NmM3NTgyNzYwZTcwOGViYzM2OWUzMDcyMzVjZTFjODUyNGMzYTNkNjg4YTYzN2JiMzIzZjYxNjhmYmZiODYiLCJpYXQiOjE2NTc1ODM0MTIuODE1MzUyLCJuYmYiOjE2NTc1ODM0MTIuODE1MzU1LCJleHAiOjQ4MTMyNTcwMTIuODEyMDAyLCJzdWIiOiIzMTkzNyIsInNjb3BlcyI6WyJlbWFpbF9mdWxsIiwiZG9tYWluc19mdWxsIiwiYWN0aXZpdHlfZnVsbCIsImFuYWx5dGljc19mdWxsIiwidG9rZW5zX2Z1bGwiLCJ3ZWJob29rc19mdWxsIiwidGVtcGxhdGVzX2Z1bGwiLCJzdXBwcmVzc2lvbnNfZnVsbCIsInNtc19mdWxsIl19.LUuRpQ0jPcIYB4uuVMlLIe9fFarxpXVjw8luRSZqFDYcyB63G68Ov0Egel02SZL0Tai5C1Z7sRAh4sRXJUX6m5LWzuk1EOZbmXqMNh6YUM0rGZFc6SgpBpkBpePZEysvbcIyG6tXMrkSZdWOMIFdGZkT3fgnbUlvXMGfrMWhv95He3g6muboaJE0QGa9aJ8lcKZ-AS5O1hHTNCW768Zu6fKqrGJdyaTZx50ECfXgLJV_0Nznr0p0ylJGreoA5Vv-9tVWD8qKPmQXaRQAKUA0gUVqIODSPx3oOUNwx5G2DXtYHVjyqIZudR-A95UqpGipU65bPJHfv9lX79QIEewWXBCqgaAo_q_0RnnzhMBJ0rDo1Pszk3CXSbtOVveNpF4Q0qDMpyfPLrNYlkF_LdxdFuuCan2lqHg8tQNav_DbG6xQ9aEtYy4HRBekElFkvG5vS3tYKvZzePHqMWpbdUOje8O7wzsBy0qBYkF0Dji92-7QIs3Hy0-HLMUz393gnY3zwmnP9j_r1IXVoKoLbC9hiIq5Oe_wgRK2efy-t7K28goh4hCZ90gkh9BfxnTeLFp9xEwP1WVLOvIvWGX78oLnvPgTx8BdMiSHGO8AyijDBS7XdL36J_aNcH1GKpfVT9RcFNMii0KbGhetpQ2Loic6w-9dImgRjg8N0-2WI_L98uY";

    //    Email.DefaultSender = new MailerSendSender(apiToken);

    //    var response = await Email
    //            .From("admin@basicondo.com.br", "Admin BasiCondo")
    //            .To("marco.torino@gmail.com", "Marco")
    //            .Subject("FluentEmail MailerSend Test")
    //            .Body("<html><body><h1>Test</h1><p>Greetings from the team, you got this message through MailerSend.</p></body></html>", true)
    //            .SendAsync()
    //            .ConfigureAwait(false);

    //    response.MessageId.Should().BeNull();
    //    response.ErrorMessages.Should().BeEmpty();
    //    response.Successful.Should().BeTrue();
    //}

    [Fact]
    public void SimpleMailFromCode()
    {
        Email.DefaultSender = new MailerSendSender(_apiToken);

        var response = Email
                .From("john@email.com", "Mailer Send Unit Test")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .Send();

        response.Successful.Should().BeTrue();
    }

    [Fact]
    public async void SimpleMailFromCodeAsync()
    {
        Email.DefaultSender = new MailerSendSender(_apiToken);

        var response = await Email
                .From("john@email.com")
                .To("bob@email.com")
                .Subject("hows it going bob")
                .Body("yo dawg, sup?")
                .SendAsync()
                .ConfigureAwait(false);

        response.Successful.Should().BeTrue();
        // response.MessageId.Should().NotBeNullOrEmpty();
        response.ErrorMessages.Should().BeEmpty();
    }

    [Fact]
    public async void SimpleMailWithNameFromCode()
    {
        Email.DefaultSender = new MailerSendSender(_apiToken);

        var response = await Email
            .From("john@email.com", "Mailer Send Unit Test")
            .To("bob@email.com")
            .Subject("hows it going bob")
            .Body("yo dawg, sup?")
            .SendAsync()
            .ConfigureAwait(false);

        response.Successful.Should().BeTrue();
        // response.MessageId.Should().NotBeNullOrEmpty();
        response.ErrorMessages.Should().BeEmpty();
    }

    [Fact]
    public async void SimpleHtmlMailFromCode()
    {
        Email.DefaultSender = new MailerSendSender(_apiToken);

        var response = await Email
            .From("john@email.com", "Mailer Send Unit Test")
            .To("bob@email.com")
            .Subject("hows it going bob")
            .Body("<html><body><h1>Test</h1></body></html>", true)
            .SendAsync()
            .ConfigureAwait(false);

        response.Successful.Should().BeTrue();
    }

    [Fact]
    public async void SimpleMailWithAttachmentFromCode()
    {
        Email.DefaultSender = new MailerSendSender(_apiToken);

        var response = await Email
            .From("john@email.com", "Mailer Send Unit Test")
            .To("bob@email.com")
            .Subject("hows it going bob")
            .Body("yo dawg, sup?")
            .Attach(new Core.Models.Attachment()
            {
                Filename = "test.txt",
                Data = new MemoryStream(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }),
                ContentType = "application/octet-stream"
            })
            .SendAsync()
            .ConfigureAwait(false);

        response.Successful.Should().BeTrue();
        // response.MessageId.Should().NotBeNullOrEmpty();
        response.ErrorMessages.Should().BeEmpty();
    }

    [Fact]
    public async Task SimpleHtmlMailWithPlaintextAlternativeBodyFromCode()
    {
        Email.DefaultSender = new MailerSendSender(_apiToken);

        var response = await Email
            .From("john@email.com", "Mailer Send Unit Test")
            .To("bob@email.com")
            .Subject("hows it going bob")
            .Body("<html><body><h1>Test</h1></body></html>", true)
            .PlaintextAlternativeBody("Test")
            .SendAsync()
            .ConfigureAwait(false);

        response.Successful.Should().BeTrue();
    }
}
