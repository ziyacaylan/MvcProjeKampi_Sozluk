using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class ScreenShot
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string  ImageName { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }
    }
}
