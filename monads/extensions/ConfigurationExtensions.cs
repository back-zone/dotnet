using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace back.zone.monads.extensions;

public static class ConfigurationExtensions
{
    /// <summary>
    ///     This extension method provides a way to convert an <see cref="IConfigurationSection" /> into a raw JSON object.
    ///     It recursively serializes the section into a dictionary, handling different data types and structures.
    /// </summary>
    /// <param name="section">The <see cref="IConfigurationSection" /> to be serialized.</param>
    /// <returns>A raw JSON object representing the section.</returns>
    public static object GetAsRawJson(this IConfigurationSection section)
    {
        var sectionDictionary =
            section
                .GetChildren()
                .ToDictionary(
                    child => child.Key,
                    SerializeSectionToJson
                );

        return sectionDictionary;
    }

    /// <summary>
    ///     A helper method to recursively serialize an <see cref="IConfigurationSection" /> into a JSON object.
    /// </summary>
    /// <param name="section">The <see cref="IConfigurationSection" /> to be serialized.</param>
    /// <returns>A JSON object representing the section.</returns>
    private static object? SerializeSectionToJson(IConfigurationSection section)
    {
        var children = section.GetChildren().ToList();

        if (children.Count != 0)
        {
            if (children.All(c => int.TryParse(c.Key, out _)))
                return children
                    .OrderBy(c => int.Parse(c.Key))
                    .Select(SerializeSectionToJson)
                    .ToList();

            return children
                .ToDictionary(
                    child => child.Key,
                    SerializeSectionToJson
                );
        }

        if (section.Value == null) return null;

        if (bool.TryParse(section.Value, out var boolValue))
            return boolValue;

        if (float.TryParse(section.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var floatValue))
            return floatValue;

        if (int.TryParse(section.Value, out var intValue))
            return intValue;

        return section.Value;
    }
}