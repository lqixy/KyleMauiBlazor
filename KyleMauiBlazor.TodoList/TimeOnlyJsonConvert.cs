using System;
using Newtonsoft.Json;

namespace KyleMauiBlazor.TodoList
{
    public class TimeOnlyJsonConvert : JsonConverter<TimeOnly>
    {
        public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.ReadAsString();
            return TimeOnly.Parse(value);
        }

        public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
        {
            value.ToString("HH:mm");
        }
    }
}

