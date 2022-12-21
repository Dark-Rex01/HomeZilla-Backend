using System;
using System.Collections.Generic;
using Final.Entities;
using Microsoft.EntityFrameworkCore;

namespace Final.Data;

public partial class HomezillaContext : DbContext
{
    public HomezillaContext()
    {
    }

    public HomezillaContext(DbContextOptions<HomezillaContext> options)
        : base(options)
    {
    }


    //Creating DbSet

    public DbSet<Authentication> Authentication { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Provider> Provider { get; set; }
    public DbSet<ProviderServices> ProviderServices { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
