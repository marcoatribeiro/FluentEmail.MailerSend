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
        private const string _baseAddress = "https://api.mailersend.com/v1/";
        private const string _emailEndPoint = "email";

        private readonly HttpClient _httpClient;
        private readonly MailerSendOptions _options = new MailerSendOptions();

        public MailerSendSender(string apiKey, Action<MailerSendOptions>? options = null)
        {
            var apiKey1 = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            options?.Invoke(_options);

            _httpClient = new HttpClient { BaseAddress = new Uri(_baseAddress) };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey1);
        }

        public SendResponse Send(IFluentEmail email, CancellationToken? token = null)
        {
            return SendAsync(email, token).GetAwaiter().GetResult();
        }

        public async Task<SendResponse> SendAsync(IFluentEmail email, CancellationToken? token = null)
        {
            var emailRequest = EmailRequest.FromEmailData(email.Data, _options);
            var httpResponse = await _httpClient.PostAsJsonAsync(_emailEndPoint, emailRequest, JsonSerializerOptions);
            var response = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            return httpResponse.IsSuccessStatusCode
                ? new SendResponse { MessageId = response }
                : new SendResponse { MessageId = ((int)httpResponse.StatusCode).ToString(), ErrorMessages = new List<string> { response } };
        }

        private static JsonSerializerOptions JsonSerializerOptions => new JsonSerializerOptions()
        {
            PropertyNamingPolicy = new SnakeCaseNamingPolicy(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
}
