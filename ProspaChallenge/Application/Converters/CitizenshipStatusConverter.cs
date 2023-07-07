using ProspaChallenge.Business.Models;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ProspaChallenge.Application.Converters
{
    public class CitizenshipStatusConverter : JsonConverter<CitizenshipStatus?>
    {
        public override CitizenshipStatus? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return (CitizenshipStatus)reader.GetInt32();
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                return value switch
                {
                    "Citizen" => CitizenshipStatus.Citizen,
                    "Permanent Resident" => CitizenshipStatus.PermanentResident,
                    _ => null
                };
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, CitizenshipStatus? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
