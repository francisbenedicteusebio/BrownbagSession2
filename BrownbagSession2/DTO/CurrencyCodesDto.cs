namespace BrownbagSession2.DTO
{
    public class CurrencyCodesDto
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public double CurrencyRate { get; set; }
        public DateTime UpdatedDateTime => DateTime.Now;
    }
}
