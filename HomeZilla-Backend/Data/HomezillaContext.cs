using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
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
        modelBuilder.Entity<OrderDetails>()
                    .HasOne(x => x.customer)
                    .WithMany(t => t.OrderDeatils)
                    .HasForeignKey(m => m.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

        modelBuilder.Entity<OrderDetails>()
                    .HasOne(m => m.provider)
                    .WithMany(t => t.OrderDeatils)
                    .HasForeignKey(m => m.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
        //OnModelCreatingPartial(modelBuilder);
    }

   // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
