using CoreAoC.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreAoC.Utils
{
    internal class CalendarDeserializer : JsonConverter<IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>>>
    {
        public override IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>>? ReadJson(JsonReader reader, Type objectType, IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JToken token = JToken.ReadFrom(reader);
            if (token.Type == JTokenType.Null)
                return new Dictionary<int, IDictionary<Problem, Tuple<Result, Result>>>();

            IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>> result = new Dictionary<int, IDictionary<Problem, Tuple<Result, Result>>>();
            foreach (JToken subToken in token.Children())
            {
                int year = int.Parse(((JProperty)subToken).Name);
                IDictionary <Problem, Tuple<Result, Result>> problems = ((JObject)subToken.Children().Single())
                    .ToObject<IDictionary<Problem, Tuple<Result, Result>>>(serializer)!;

                result.Add(year, problems);
            }
                
            return result;
        }

        public override void WriteJson(JsonWriter writer, IDictionary<int, IDictionary<Problem, Tuple<Result, Result>>>? value, JsonSerializer serializer)
            => throw new NotImplementedException("Class for read-only");
    }
}
