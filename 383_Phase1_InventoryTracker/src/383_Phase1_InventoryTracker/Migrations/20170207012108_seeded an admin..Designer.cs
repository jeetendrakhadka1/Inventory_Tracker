using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using _383_Phase1_InventoryTracker.Entities;

namespace _383_Phase1_InventoryTracker.Migrations
{
    [DbContext(typeof(InventoryTrackerContext))]
    [Migration("20170207012108_seeded an admin.")]
    partial class seededanadmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_383_Phase1_InventoryTracker.Entities.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedByUserId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("_383_Phase1_InventoryTracker.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Role");

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
