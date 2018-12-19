using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwell.Domain.Entities
{
    [Table("City")]
    public class City
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public string District { get; set; }

        public int Population { get; set; }
    }
}
