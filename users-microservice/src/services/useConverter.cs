using System.Text.Json;
using System.Text.Json.Serialization;
using users_microservice.models;
namespace users_microservice.services
{
    public class UserConverter : JsonConverter<UserModel>
    {
        public override UserModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;
                var role = (Role)root.GetProperty("Role").GetInt32();
                switch (role)
                {
                    case Role.ADMIN:
                        var admin = JsonSerializer.Deserialize<Admin>(root.GetRawText(), options);
                        if (admin == null)
                        {
                            throw new JsonException("Failed to deserialize Admin object");
                        }
                        return admin;
                    case Role.STUDENT:
                        var student = JsonSerializer.Deserialize<Student>(root.GetRawText(), options);
                        if (student == null)
                        {
                            throw new JsonException("Failed to deserialize Student object");
                        }
                        return student;
                    default:
                        throw new JsonException("Unsupported role type");
                }
            }
        }
        public override void Write(Utf8JsonWriter writer, UserModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}