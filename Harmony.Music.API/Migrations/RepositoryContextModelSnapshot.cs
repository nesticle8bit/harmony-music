﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Harmony.Music.Repository;
using Harmony.Music.Shared.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Harmony.Music.API.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Harmony.Music.Entities.Music.Album", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Artwork")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Disc")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Genres")
                        .HasColumnType("jsonb");

                    b.Property<string>("MusicBrainzDiscId")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Title")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("albums", "music");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.Artist", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FoundedIn")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("MetalArchivesUrl")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Name")
                        .HasMaxLength(400)
                        .HasColumnType("character varying(400)");

                    b.Property<List<string>>("Tags")
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("artists", "music");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.ArtistAlbums", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AlbumId")
                        .HasColumnType("bigint");

                    b.Property<long>("ArtistId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("artistsAlbums", "music");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.Library", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(45)
                        .HasColumnType("character varying(45)");

                    b.Property<bool>("AlbumColors")
                        .HasColumnType("boolean");

                    b.Property<bool>("AlbumImages")
                        .HasColumnType("boolean");

                    b.Property<bool>("ArtistImages")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("HasBeenProcessed")
                        .HasColumnType("boolean");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<bool>("ReadingFiles")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("library", "music");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.Song", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AlbumId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Lyrics")
                        .HasColumnType("text");

                    b.Property<MediaPropertyDto>("MediaProperties")
                        .HasColumnType("jsonb");

                    b.Property<string>("Mimetype")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("PossiblyCorrupt")
                        .HasColumnType("boolean");

                    b.Property<int>("Track")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("songs", "music");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.ArtistAlbums", b =>
                {
                    b.HasOne("Harmony.Music.Entities.Music.Album", "Album")
                        .WithMany("ArtistAlbums")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Harmony.Music.Entities.Music.Artist", "Artist")
                        .WithMany("ArtistAlbums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.Song", b =>
                {
                    b.HasOne("Harmony.Music.Entities.Music.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.Album", b =>
                {
                    b.Navigation("ArtistAlbums");

                    b.Navigation("Songs");
                });

            modelBuilder.Entity("Harmony.Music.Entities.Music.Artist", b =>
                {
                    b.Navigation("ArtistAlbums");
                });
#pragma warning restore 612, 618
        }
    }
}
