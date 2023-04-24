using Microsoft.FSharp.Core;

namespace PracticalAstronomy.CSharp;

internal static class FSharpHelper
{
    public static T? OptionToNullable<T>(this FSharpOption<T> option) where T : class
    {
        if (FSharpOption<T>.get_IsNone(option))
        {
            return null;
        }
        else
        {
            return option.Value;
        }
    }
}
