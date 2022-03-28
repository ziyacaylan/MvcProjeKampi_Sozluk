using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concreate
{
    public class Talent
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string SkillName { get; set; }
        [StringLength(250)]
        public string SkillDetails { get; set; }
        public int SkillLevel { get; set; }
        public string SkillArea { get; set; }
    }
}
