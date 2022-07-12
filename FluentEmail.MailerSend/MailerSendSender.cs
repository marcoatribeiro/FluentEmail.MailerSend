using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;
using FluentEmail.MailerSend.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace FluentEmail.MailerSend
{
    public class MailerSendSender : ISender
    {
        private const string _emailEndPoint = "email";

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public MailerSendSender(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.mailersend.com/v1/")
            };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public SendResponse Send(IFluentEmail email, CancellationToken? token = null)
        {
            return SendAsync(email, token).GetAwaiter().GetResult();
        }

        public async Task<SendResponse> SendAsync(IFluentEmail email, CancellationToken? token = null)
        {
            /*
            var parameters = new List<KeyValuePair<string, string>>();

            parameters.Add(new KeyValuePair<string, string>("from", $"{email.Data.FromAddress.Name} <{email.Data.FromAddress.EmailAddress}>"));
            email.Data.ToAddresses.ForEach(x => {
                parameters.Add(new KeyValuePair<string, string>("to", $"{x.Name} <{x.EmailAddress}>"));
            });
            email.Data.CcAddresses.ForEach(x => {
                parameters.Add(new KeyValuePair<string, string>("cc", $"{x.Name} <{x.EmailAddress}>"));
            });
            email.Data.BccAddresses.ForEach(x => {
                parameters.Add(new KeyValuePair<string, string>("bcc", $"{x.Name} <{x.EmailAddress}>"));
            });
            email.Data.ReplyToAddresses.ForEach(x => {
                parameters.Add(new KeyValuePair<string, string>("h:Reply-To", $"{x.Name} <{x.EmailAddress}>"));
            });
            parameters.Add(new KeyValuePair<string, string>("subject", email.Data.Subject));

            parameters.Add(new KeyValuePair<string, string>(email.Data.IsHtml ? "html" : "text", email.Data.Body));

            if (!string.IsNullOrEmpty(email.Data.PlaintextAlternativeBody))
            {
                parameters.Add(new KeyValuePair<string, string>("text", email.Data.PlaintextAlternativeBody));
            }

            email.Data.Tags.ForEach(x =>
            {
                parameters.Add(new KeyValuePair<string, string>("o:tag", x));
            });

            foreach (var emailHeader in email.Data.Headers)
            {
                var key = emailHeader.Key;
                if (!key.StartsWith("h:"))
                {
                    key = "h:" + emailHeader.Key;
                }

                parameters.Add(new KeyValuePair<string, string>(key, emailHeader.Value));
            }

            var files = new List<HttpFile>();
            email.Data.Attachments.ForEach(x =>
            {
                string param;

                if (x.IsInline)
                    param = "inline";
                else
                    param = "attachment";

                files.Add(new HttpFile
                {
                    ParameterName = param,
                    Data = x.Data,
                    Filename = x.Filename,
                    ContentType = x.ContentType
                });
            });

            var response = await _httpClient.PostMultipart<MailgunResponse>("messages", parameters, files).ConfigureAwait(false);

            var result = new SendResponse { MessageId = response.Data?.Id };
            if (!response.Success)
            {
                result.ErrorMessages.AddRange(response.Errors.Select(x => x.ErrorMessage));
                return result;
            }

            return result;
            */

            var from = new
            {
                Email = email.Data.FromAddress.EmailAddress,
                Name = email.Data.FromAddress.Name
            };

            var to = email.Data.ToAddresses.ToRecipients();
            var cc = email.Data.CcAddresses.ToRecipients();
            var bcc = email.Data.BccAddresses.ToRecipients();
            var replyTo = email.Data.ReplyToAddresses.ToRecipients();

            var attachments = email.Data.Attachments.ToAttachments();

            /*
            var variables = email.Data.ToAddresses.Select(to => new
            {
                Email = to.Email,
                Substitutions = to?.Substitutions?
                    .Select(kvp => new { Var = kvp.Key, kvp.Value })
            });

            if (email.Data.CcAddresses.Any())
            {
                variables = variables.Concat(email.Data.CcAddresses.Select(recipient => new
                {
                    recipient.Email,
                    Substitutions = recipient?.Substitutions?
                   .Select(kvp => new { Var = kvp.Key, kvp.Value })
                }));
            }

            if (email.Data.BccAddresses.Any())
            {
                variables = variables.Concat(email.Data.BccAddresses.Select(recipient => new
                {
                    recipient.Email,
                    Substitutions = recipient?.Substitutions?
                   .Select(kvp => new { Var = kvp.Key, kvp.Value })
                }));
            }
            */

            /*
            var sendAtUts = (int?)sendAt?
                .ToUniversalTime()
                .Subtract(DateTime.UnixEpoch)
                .TotalSeconds;
            */

            var requestBody = new
            {
                From = from,
                To = to,
                Cc = cc,
                Bcc = bcc,
                Subject = email.Data.Subject,
                Text = email.Data.IsHtml ? email.Data.PlaintextAlternativeBody : email.Data.Body,
                Html = email.Data.IsHtml ? email.Data.Body : null,
                // Template_id = templateId,
                // Attachments = attachments,
                // Variables = variables,
                // Send_at = sendAtUts
            };

            var httpResponse = await _httpClient.PostAsJsonAsync(_emailEndPoint, requestBody, GetJsonSerializerOptions());

            if (httpResponse.IsSuccessStatusCode)
            {
                return new SendResponse();
            }

            var response = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new SendResponse { MessageId = ((int)httpResponse.StatusCode).ToString(), ErrorMessages = new List<string> { response } };
        }

        private static JsonSerializerOptions GetJsonSerializerOptions() => new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}
