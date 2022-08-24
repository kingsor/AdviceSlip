using System.ComponentModel.DataAnnotations;

namespace AdviceSlipService.Attributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minValue;

        public MinValueAttribute(int minValue)
        {
            _minValue = minValue;
            ErrorMessage = "Enter a value greater than or equal to " + _minValue;
        }

        public override bool IsValid(object value)
        {
            if((int)value == int.MinValue)
            {
                return true;
            }

            return (int)value >= _minValue;
        }
    }
}
