using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UCHEBMDK11._01.Models;

namespace UCHEBMDK11._01
{
    internal class AppContext:DbContext
    {
        public DbSet<Legal_document> legal_document { get; set; }
        public DbSet<Sides_of_contract> sides_of_contract { get; set; }
        public DbSet<Document_status> document_status { get; set; }
        public DbSet<Document_type> document_type { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }

        public DbSet<ActiveDocumentView> ActiveDocumentsView { get; set; }
        public AppContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=legalDB;Username=postgres;Password=2133");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveDocumentView>()
                .ToView("active_documents_view")
                .HasNoKey();
        }
    }
}
