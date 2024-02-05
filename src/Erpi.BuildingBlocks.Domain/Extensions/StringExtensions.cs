using System.Text.RegularExpressions;

namespace Erpi.BuildingBlocks.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsAlphanumeric(this string input)
    {
        string pattern = "^[a-zA-Z0-9]*$";
        return Regex.IsMatch(input, pattern);
    }
}