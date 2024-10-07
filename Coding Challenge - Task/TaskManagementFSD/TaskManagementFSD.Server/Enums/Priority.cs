using System.Text.Json.Serialization;

namespace TaskManagementFSD.Server.Enums
{
    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
