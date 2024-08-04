using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace back.zone.monads.extensions;

public static class ConfigurationExtensions
{
    public static object GetRawJson(this IConfigurationSection section)
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

        if (double.TryParse(section.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
            return doubleValue;

        if (int.TryParse(section.Value, out var intValue))
            return intValue;

        return section.Value;
    }
}