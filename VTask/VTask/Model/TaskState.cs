using System.Text.Json.Serialization;

namespace VTask.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TaskState
    {
        Inactive,
        Active,
        Finished,
        Archived
    }
}
