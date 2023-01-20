namespace HomeZilla_Backend.Models.Analytics
{
    public class DoughChartResponse
    {
        public string Label { get; set; }
        public int Data { get; set; }

    }
    public class DoughChartResponses
    {
        public List<DoughChartResponse> Responses { get; set; }
    }
}
