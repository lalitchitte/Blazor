using System.Text.Json.Serialization;

namespace BlazorProjectSharedLibrary
{
    public class ApiResponse<T>
    {
        public bool Result { get; set; }

        public string Reason { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }
    }
}
