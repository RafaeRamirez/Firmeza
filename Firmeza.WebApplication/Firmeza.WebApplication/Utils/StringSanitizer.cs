using Firmeza.WebApplication.Interfaces;
using System.Text.RegularExpressions;

namespace Firmeza.WebApplication.Utils;
public sealed class StringSanitizer : IStringSanitizer
{
    public string NormalizeForSearch(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;
        var t = input.Trim().ToLowerInvariant();
        return Regex.Replace(t, "\\s+", " ");
    }
}