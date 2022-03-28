using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concreate;

namespace DataAccessLayer.Concreate
{
    public class Context : DbContext      // ilk başta access modifier kısmını public yapıyoruz ki diğer taraflardan ulaşabilelim. DbContext sınıfından kalıtım alıyor
    {
        public DbSet<About> Abouts { get; set; } // "Abouts" sınıfın veri tabanına yansıyacak ismini göstermektedir.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Heading> Headings { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Talent> Talents { get; set; }
    }
}