
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }

        [StringLength(500)]
        public string AboutTitle { get; set; }

        [StringLength(1000)]
        public string AboutDetails { get; set; }
    }
}