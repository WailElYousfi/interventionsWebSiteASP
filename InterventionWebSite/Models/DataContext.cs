using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace InterventionWebSite.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("StringDBContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>().HasMany(x => x.Interventions).WithOptional(x => x.Request).WillCascadeOnDelete();
            modelBuilder.Entity<Role>().HasMany(x => x.Users).WithOptional(x => x.Role).WillCascadeOnDelete();
            modelBuilder.Entity<Agency>().HasMany(x => x.Staffs).WithOptional(x => x.Agency).WillCascadeOnDelete();
            modelBuilder.Entity<Agency>().HasMany(x => x.Interventions).WithOptional(x => x.Agency).WillCascadeOnDelete();
            modelBuilder.Entity<Direction>().HasMany(x => x.Interventions).WithOptional(x => x.Direction).WillCascadeOnDelete();
            modelBuilder.Entity<Direction>().HasMany(x => x.Staffs).WithOptional(x => x.Direction).WillCascadeOnDelete();

            modelBuilder.Entity<User>().HasMany(x => x.InterventionsAssignment).WithOptional(x => x.AssignmentManager).HasForeignKey(x=>x.AssignmentManagerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(x => x.InterventionssIntervener).WithOptional(x => x.Intervener).HasForeignKey(x=>x.IntervenerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<User>().HasMany(x => x.Requests).WithOptional(x => x.ReceptionEmployee).HasForeignKey(x=>x.ReceptionEmployeeId).WillCascadeOnDelete();
            //modelBuilder.Entity<User>().HasMany(x => x.InterventionssIntervener).WithOptional(x => x.Intervener).WillCascadeOnDelete();
           // modelBuilder.Entity<User>().HasMany(x => x.Requests).WithOptional(x => x.ReceptionEmployee).WillCascadeOnDelete();
            modelBuilder.Entity<State>().HasMany(x => x.interventions).WithOptional(x => x.State).WillCascadeOnDelete();
           // modelBuilder.Entity<Staff>().HasMany(x => x.Requests).WithOptional(x => x.Staff).WillCascadeOnDelete();

          //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Role> roles { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<Agency> agencies { get; set; }
        public DbSet<Direction> directions { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Intervention> interventions { get; set; }
        public object DeleteBehavior { get; private set; }

        //   public System.Data.Entity.DbSet<InterventionWebSite.Models.LoginViewModel> LoginViewModels { get; set; }
    }
}