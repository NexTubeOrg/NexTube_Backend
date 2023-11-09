﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NexTube.Persistence.Data.Contexts;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NexTube.Persistence.Migrations.VideoDb
{
    [DbContext(typeof(VideoDbContext))]
    partial class VideoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NexTube.Domain.Entities.VideoCommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("VideoEntityId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("VideoEntityId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("NexTube.Domain.Entities.VideoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PreviewPhotoId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("VideoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("NexTube.Domain.Entities.VideoCommentEntity", b =>
                {
                    b.HasOne("NexTube.Domain.Entities.VideoEntity", "VideoEntity")
                        .WithMany()
                        .HasForeignKey("VideoEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("VideoEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
