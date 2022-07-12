using System.Text.Json;

namespace FluentEmail.MailerSend.Utils
{
    internal class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}
