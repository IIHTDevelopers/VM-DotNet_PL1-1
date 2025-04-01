namespace DotNetSelenium.Utilities
{
    public class TestResults
    {
        public Dictionary<string, TestCaseResultDto> TestCaseResults { get; set; }
        public string CustomData { get; set; }
        public string AttemptId { get; set; }
        public string HostName { get; set; }
        public string FilePath { get; set; }
    }
}
