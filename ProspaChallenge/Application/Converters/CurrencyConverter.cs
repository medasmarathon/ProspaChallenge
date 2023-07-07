using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ProspaChallenge.Application.Converters
{
    public class CurrencyConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDecimal();
            }
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            return decimal.Parse(reader.GetString()!, NumberStyles.Currency, culture);
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-US");
            writer.WriteStringValue(value.ToString(culture));
        }
    }
}
