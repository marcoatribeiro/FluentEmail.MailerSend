using System.Collections.Generic;

namespace FluentEmail.MailerSend
{
    public class Variable
    {
        public string Email { get; set; } = default!;
        public IList<Substitution> Substituitions { get; set; } = new List<Substitution>();
    }
}
