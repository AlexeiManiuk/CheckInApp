namespace CheckInEF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Table("LoginInfo")]
    public partial class LoginInfo
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [XmlElement("Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        [XmlElement("Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [XmlElement("Email")]
        public string Email { get; set; }
    }
}
