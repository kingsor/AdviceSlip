using AdviceSlipService.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AdviceSlipService.Models
{
    public class AdviceRequest
    {
        [Required]
        public string Topic { get; set; } = string.Empty;
        [MinValue(1)]
        public int Amount { get; set; } = int.MinValue;
    }
}
