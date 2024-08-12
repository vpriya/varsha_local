using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cards.API.Models
{
    public class Card
    {

        [Key]
        public Guid Id { get; set; }

        [Column (TypeName = "varchar(60)")]
        public string? CardholderName { get; set; }

        [Column(TypeName = "varchar(16)")]
        public string? CardNumber { get; set; }

        [Column(TypeName = "varchar(2)")]
        public int ExpiryMonth { get; set; }

        [Column(TypeName = "varchar(2)")]
        public int ExpiryYear { get; set; }

        [Column(TypeName = "varchar(3)")]
        public int CVC { get; set; }

    }
}
