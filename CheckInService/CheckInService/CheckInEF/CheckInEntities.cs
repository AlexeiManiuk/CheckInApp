namespace CheckInEF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CheckInEntities : DbContext
    {
        public CheckInEntities()
            : base("name=CheckInDB.mdf")
        {
        }

        public virtual DbSet<LoginInfo> LoginInfoes { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Coordinates> Coordinates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
