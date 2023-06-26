using System.ComponentModel.DataAnnotations;

namespace Cards.API.Models
{
    public class Card
    {
        public Card(char name, char number)
        {
            V1 = name;
            V2 = number;
        }

        [Key]
        public Guid Id { get; set; }
        public string? CardholderName { get; set; }
        public string? CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVC { get; set; }
        public char V1 { get; }
        public char V2 { get; }
    }
}
