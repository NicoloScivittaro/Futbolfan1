﻿// <auto-generated />
using System;
using FutbolFan1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Futbolfan1.Server.Migrations
{
    [DbContext(typeof(FutbolFanContext))]
    partial class FutbolFanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChampionshipTeam", b =>
                {
                    b.Property<int>("ChampionshipId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("ChampionshipId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("ChampionshipTeams");
                });

            modelBuilder.Entity("Formation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Formations");
                });

            modelBuilder.Entity("FutbolFan1.Models.Championship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ChampionshipId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Championships");
                });

            modelBuilder.Entity("FutbolFan1.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Defense")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Dribbling")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsStarting")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Passing")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Physical")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Shooting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Speed")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("FutbolFan1.Models.PlayerSave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeamSaveId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamSaveId");

                    b.ToTable("PlayerSaves");
                });

            modelBuilder.Entity("FutbolFan1.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("FormationId")
                        .HasColumnType("int");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormationId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("FutbolFan1.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Coach")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FormationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Overall")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SalaryBudget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TransferBudget")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("FormationId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FutbolFan1.Models.TeamSave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("SalaryBudget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SaveName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SavedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.Property<decimal>("TransferBudget")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamSaves");
                });

            modelBuilder.Entity("FutbolFan1.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FavoriteTeam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ChampionshipTeam", b =>
                {
                    b.HasOne("FutbolFan1.Models.Championship", "Championship")
                        .WithMany("ChampionshipTeams")
                        .HasForeignKey("ChampionshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FutbolFan1.Models.Team", "Team")
                        .WithMany("ChampionshipTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Championship");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FutbolFan1.Models.Player", b =>
                {
                    b.HasOne("FutbolFan1.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("FutbolFan1.Models.PlayerSave", b =>
                {
                    b.HasOne("FutbolFan1.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FutbolFan1.Models.TeamSave", "TeamSave")
                        .WithMany("PlayerSaves")
                        .HasForeignKey("TeamSaveId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("TeamSave");
                });

            modelBuilder.Entity("FutbolFan1.Models.Position", b =>
                {
                    b.HasOne("Formation", null)
                        .WithMany("Positions")
                        .HasForeignKey("FormationId");
                });

            modelBuilder.Entity("FutbolFan1.Models.Team", b =>
                {
                    b.HasOne("Formation", null)
                        .WithMany("Teams")
                        .HasForeignKey("FormationId");
                });

            modelBuilder.Entity("FutbolFan1.Models.TeamSave", b =>
                {
                    b.HasOne("FutbolFan1.Models.Team", "Team")
                        .WithMany("TeamSaves")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Formation", b =>
                {
                    b.Navigation("Positions");

                    b.Navigation("Teams");
                });

            modelBuilder.Entity("FutbolFan1.Models.Championship", b =>
                {
                    b.Navigation("ChampionshipTeams");
                });

            modelBuilder.Entity("FutbolFan1.Models.Team", b =>
                {
                    b.Navigation("ChampionshipTeams");

                    b.Navigation("Players");

                    b.Navigation("TeamSaves");
                });

            modelBuilder.Entity("FutbolFan1.Models.TeamSave", b =>
                {
                    b.Navigation("PlayerSaves");
                });
#pragma warning restore 612, 618
        }
    }
}
