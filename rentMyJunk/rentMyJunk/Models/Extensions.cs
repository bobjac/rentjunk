using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace rentMyJunk.Models
{
    public static class Extensions
    {
        public static int ToEpoch(this DateTime date)
        {
            if (date == null) return int.MinValue;
            DateTime epoch = new DateTime(1970, 1, 1);
            TimeSpan epochTimeSpan = date - epoch;
            return (int)epochTimeSpan.TotalSeconds;
        }
    }

    public class EpochDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            int seconds;
            if (value is DateTime)
            {
                DateTime dt = (DateTime)value;
                if (!dt.Equals(DateTime.MinValue))
                    seconds = dt.ToEpoch();
                else
                    seconds = int.MinValue;
            }
            else
            {
                throw new Exception("Expected date object value.");
            }

            writer.WriteValue(seconds);
        }

        public override object ReadJson(JsonReader reader, Type type, object value, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.None || reader.TokenType == JsonToken.Null)
                return null;

            if (reader.TokenType != JsonToken.Integer)
            {
                throw new Exception(
                    String.Format("Unexpected token parsing date. Expected Integer, got {0}.",
                    reader.TokenType));
            }

            int seconds = (int)reader.Value;
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        }
    }
}