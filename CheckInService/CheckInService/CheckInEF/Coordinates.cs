using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace CheckInEF
{
    [Table("Coordinates")]
    public partial class Coordinates
    {
        public int Id { get; set; }

        [Required]
        [XmlElement("Latitude")]
        public double Latitude { get; set; }

        [Required]
        [XmlElement("Longitude")]
        public double Longitude { get; set; }

        [Required]
        [XmlElement("UserFK")]
        public string UserFK { get; set; }
    }
}
