namespace NalysaDeveloper.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DevContext : DbContext
    {
        // Your context has been configured to use a 'Devs' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'NalysaDeveloper.Models.Devs' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Devs' 
        // connection string in the application configuration file.
        public DevContext()
            : base("name=Devs")
        {
        }
        public DbSet<Developers> Developers { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<SkillLists> SkillLists { get; set; }

        

        
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}