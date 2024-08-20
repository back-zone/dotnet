using System.Collections.ObjectModel;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace back.zone.core.Serde.Json;

public static class ConfigurationExtensions
{
    /// <summary>
    ///     This extension method provides a way to serialize an <see cref="IConfigurationSection" /> into a raw JSON object.
    /// </summary>
    /// <param name="section">The <see cref="IConfigurationSection" /> to be serialized.</param>
    /// <returns>
    ///     A JSON object representing the serialized <paramref name="section" />, or an empty dictionary if the section
    ///     is empty.
    /// </returns>
    public static object GetAsRawJson(this IConfigurationSection section)
    {
        return SerializeSectionToJson(section) ?? new Dictionary<string, object?>();
    }

    private static object? SerializeSectionToJson(IConfigurationSection section)
    {
        var children = section.GetChildren().ToList();

        if (children.Count <= 0) return ConvertSectionValue(section.Value);

        return IsNumericKeys(children)
            ? SerializeNumericKeyedSection(children)
            : SerializeDictionarySection(children);
    }

    private static bool IsNumericKeys(List<IConfigurationSection> children)
    {
        return children.All(c => int.TryParse(c.Key, out _));
    }

    private static List<object?> SerializeNumericKeyedSection(List<IConfigurationSection> children)
    {
        return children
            .OrderBy(c => int.Parse(c.Key))
            .Select(SerializeSectionToJson)
            .ToList();
    }

    private static ReadOnlyDictionary<string, object?> SerializeDictionarySection(List<IConfigurationSection> children)
    {
        return new ReadOnlyDictionary<string, object?>(
            children.ToDictionary(child => child.Key, SerializeSectionToJson)
        );
    }

    private static object? ConvertSectionValue(string? value)
    {
        if (value == null) return null;

        if (bool.TryParse(value, out var boolValue))
            return boolValue;

        if (float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var floatValue))
            return floatValue;

        if (int.TryParse(value, out var intValue))
            return intValue;

        return value;
    }
}