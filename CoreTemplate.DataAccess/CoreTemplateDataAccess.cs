using CoreTemplate.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.DataAccess
{
    public class CoreTemplateDataAccess: DbContext
    {
        public CoreTemplateDataAccess(DbContextOptions<CoreTemplateDataAccess> options)
            : base(options)
        {
        }

        public DbSet<TableModel> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TableModel>().ToTable("TableModel");
        }
    }
}
