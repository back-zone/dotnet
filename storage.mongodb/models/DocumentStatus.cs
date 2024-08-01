using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace back.zone.storage.mongodb.models;

public static class DocumentStatusSchema
{
    public const string IsActive = "is_active";
    public const string IsDeleted = "is_deleted";
    public const string UpdatedAt = "updated_at";
    public const string CreatedAt = "created_at";
}

public sealed record DocumentStatus(
    [property: JsonPropertyName(DocumentStatusSchema.IsActive)]
    [property: BsonElement(DocumentStatusSchema.IsActive)]
    bool IsActive,
    [property: JsonPropertyName(DocumentStatusSchema.IsDeleted)]
    [property: BsonElement(DocumentStatusSchema.IsDeleted)]
    bool IsDeleted,
    [property: JsonPropertyName(DocumentStatusSchema.UpdatedAt)]
    [property: BsonElement(DocumentStatusSchema.UpdatedAt)]
    DateTime UpdatedAt,
    [property: JsonPropertyName(DocumentStatusSchema.CreatedAt)]
    [property: BsonElement(DocumentStatusSchema.CreatedAt)]
    DateTime CreatedAt
)
{
    public static DocumentStatus Default => Make(true, false);

    public static DocumentStatus Make(bool isActive, bool isDeleted)
    {
        return new DocumentStatus(isActive, isDeleted, DateTime.UtcNow, DateTime.UtcNow);
    }
}