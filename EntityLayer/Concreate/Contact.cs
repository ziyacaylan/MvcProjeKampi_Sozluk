
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntityLayer.Concreate
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string UserMail { get; set; }
        [StringLength(50)]
        public string UserSubject { get; set; }
        [AllowHtml] //bu tanım mesaj içerisinde çeşitli kompanentler kullanmamıza olanak sağlayacak
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsRead { get; set; }
        public bool IsImportant { get; set; }

    }
}