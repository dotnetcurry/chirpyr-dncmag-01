using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ChirpyR.Data.Context
{
    public class ChirpyRDataContext : DbContext
    {
        string _connectionName, _schemaName;
        public ChirpyRDataContext(string connectionName, string schemaName)
            : base(connectionName)
        {
            _connectionName = connectionName;
            _schemaName = schemaName;
        }
        public DbSet<ChirpyR.Data.Model.Chirp> Chirps { get; set; }
        public DbSet<ChirpyR.Data.Model.ChirpyRUser> ChirpyRUsers { get; set; }
        public DbSet<ChirpyR.Data.Model.ChirpTag> ChirpTags { get; set; }
        public DbSet<ChirpyR.Data.Model.ChirpyRRelation> ChirpyRRelations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ChirpyRDataContext>(new CreateDatabaseIfNotExists<ChirpyRDataContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
