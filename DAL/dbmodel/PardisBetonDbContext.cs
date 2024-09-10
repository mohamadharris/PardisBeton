using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.dbmodel;

public partial class PardisBetonDbContext : IdentityDbContext
{
    public PardisBetonDbContext()
    {
    }

    public PardisBetonDbContext(DbContextOptions<PardisBetonDbContext> options)
        : base(options)
    {
    }

   
    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostCategory> PostCategories { get; set; }

    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<About> Abouts { get; set; }
    public virtual DbSet<Personnel> Personnels { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


}
