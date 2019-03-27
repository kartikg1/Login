using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Resume_Builder.Models
{
    public class Contact
    {
        [Key]
        public string Bname { get; set; }
        public string Bemail { get; set; }
        public string Bmobile { get; set; }
        public string Bsubject { get; set; }
        public string Bmessage { get; set; }
    }
}