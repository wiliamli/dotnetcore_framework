using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jwell.Domian.Entities
{
    [Table("province")]
    public class Province
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
