using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RightsType> RightsTypes { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<ClaimMonth> ClaimMonths { get; set; }
        public DbSet<BudgetYear> BudgetYears { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ClaimsDetail> ClaimsDetails { get; set; }
    }
}