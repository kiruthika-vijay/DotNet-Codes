using System.Text.Json.Serialization;

namespace CodingChallengeTask.Enums
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
