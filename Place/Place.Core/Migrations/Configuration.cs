using System.Data.Entity.Migrations;
using Place.Core.Data;

namespace Place.Core.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //uncomment for local use only
            //AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(ApplicationDbContext context)
        {
            CreateIfNotExistWithSeed.InitializeDb(context);
            //base.Seed(context);
        }
    }
}
