using ShortestPath.BusinessLogic.Entities;

namespace ShortestPath.BusinessLogic.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

public class ActorConverter : JsonConverter<Actor>
{
    public override Actor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string? token = reader.GetString();
            
            if (!string.IsNullOrWhiteSpace(token))
                return new Actor(reader.GetString()!);
        }
        throw new JsonException("Expected string for actor name.");
    }

    public override void Write(Utf8JsonWriter writer, Actor value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Name);
    }
}