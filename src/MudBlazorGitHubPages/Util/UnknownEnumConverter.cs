using System.Text.Json;
using System.Text.Json.Serialization;
using MudBlazorGitHubPages.Util.Generic;

namespace MudBlazorGitHubPages.Util;

/// <summary>
///     Copied from https://gaevoy.com/2023/09/26/dotnet-serialization-unknown-enums-handling-api.html
/// </summary>
public class UnknownEnumConverter : JsonConverterFactory
{
    private readonly JsonStringEnumConverter _underlying = new();

    public sealed override bool CanConvert(Type enumType)
        => _underlying.CanConvert(enumType);

    public sealed override JsonConverter CreateConverter(Type enumType, JsonSerializerOptions options)
    {
        var underlyingConverter = _underlying.CreateConverter(enumType, options);
        var converterType = typeof(UnknownEnumConverter<>).MakeGenericType(enumType);
        return (JsonConverter)Activator.CreateInstance(converterType, underlyingConverter)!;
    }
}