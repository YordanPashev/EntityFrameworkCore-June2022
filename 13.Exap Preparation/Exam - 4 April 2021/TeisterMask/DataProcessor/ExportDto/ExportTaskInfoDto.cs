namespace TeisterMask.DataProcessor.ExportDto
{
    using Newtonsoft.Json;

    [JsonObject]
    public class ExportTaskInfoDto
    {
        [JsonProperty("TaskName")]
        public string TaskName { get; set; }

        [JsonProperty("OpenDate")]
        public string OpenDate { get; set; }

        [JsonProperty("DueDate")]
        public string DueDate { get; set; }

        [JsonProperty("LabelType")]
        public string ExecutionType { get; set; }

        [JsonProperty("ExecutionType")]
        public string LabelType { get; set; }
    }
}
