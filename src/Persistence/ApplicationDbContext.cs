﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Business.Abstractions.Data;
using Domain.Entities;

namespace Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
}