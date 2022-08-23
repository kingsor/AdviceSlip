namespace AdviceSlipService.Models
{
    public class AdviceResponse
    {
        public AdviceResponse(List<string> adviceList)
        {
            AdviceList = adviceList;
        }

        public IEnumerable<string> AdviceList { get; }
    }
}
