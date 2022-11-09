using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForeignNationalAPI.Models
{
    public class FNDetail
    {
        [Key]
        public int FNDetailId { get; set; }
        
        [Column (TypeName="nvarchar(30)")]
        public string Lastname { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Firstname  { get; set; }

        [Column(TypeName = "nvarchar(80)")]
        public string FNemail { get; set; }

        [Column(TypeName = "nvarchar(7)")]
        public string FNgender { get; set; }

        [Column(TypeName = "nvarchar(8)")]
        public string DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; }

    }
}
