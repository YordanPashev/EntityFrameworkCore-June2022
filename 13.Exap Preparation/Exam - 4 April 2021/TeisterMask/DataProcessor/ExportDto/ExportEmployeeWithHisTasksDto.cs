namespace TeisterMask.DataProcessor.ExportDto
{

    using Newtonsoft.Json;

    [JsonObject]
    public class ExportEmployeeWithHisTasksDto
    {
        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("Tasks")]
        public ExportTaskInfoDto[] Tasks { get; set; }
    }
}
