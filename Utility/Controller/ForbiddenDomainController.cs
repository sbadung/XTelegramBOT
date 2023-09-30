namespace XTelegramBOT.Utility
{
    public class ForbiddenDomainController
    {
        public static bool ContainsForbiddenDomain(string text)
        {
            return Configuration.Default.FORBIDDEN_DOMAINS.Any(domain => text.Equals(domain, StringComparison.OrdinalIgnoreCase));
        }
    }
}