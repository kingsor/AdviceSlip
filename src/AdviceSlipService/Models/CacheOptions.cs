namespace AdviceSlipService.Models
{
    public class CacheOptions
    {
        public int SlidingExpirationSecs { get; set; }
        public int AbsoluteExpirationSecs { get; set; }
    }
}
