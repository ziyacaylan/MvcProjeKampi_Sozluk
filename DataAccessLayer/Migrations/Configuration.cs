﻿namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Concreate.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; // Bizim migrationlar üzerinde değişiklik yapabilmemiz için "false" gelen bu seçeneği "true" yapmamız gerekiyor.
        }

        protected override void Seed(DataAccessLayer.Concreate.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
