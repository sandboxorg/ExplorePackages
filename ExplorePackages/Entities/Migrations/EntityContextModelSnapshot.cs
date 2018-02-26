﻿// <auto-generated />
using Knapcode.ExplorePackages.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Knapcode.ExplorePackages.Entities.Migrations
{
    [DbContext(typeof(EntityContext))]
    partial class EntityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogCommitEntity", b =>
                {
                    b.Property<long>("CatalogCommitKey")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CatalogPageKey");

                    b.Property<string>("CommitId")
                        .IsRequired();

                    b.Property<long>("CommitTimestamp");

                    b.Property<int>("Count");

                    b.HasKey("CatalogCommitKey");

                    b.HasIndex("CatalogPageKey");

                    b.HasIndex("CommitId")
                        .IsUnique();

                    b.HasIndex("CommitTimestamp")
                        .IsUnique();

                    b.ToTable("CatalogCommits");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogLeafEntity", b =>
                {
                    b.Property<long>("CatalogLeafKey")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CatalogCommitKey");

                    b.Property<long>("PackageKey");

                    b.Property<string>("RelativePath");

                    b.Property<int>("Type");

                    b.HasKey("CatalogLeafKey");

                    b.HasIndex("CatalogCommitKey");

                    b.HasIndex("PackageKey");

                    b.ToTable("CatalogLeaves");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogPackageEntity", b =>
                {
                    b.Property<long>("PackageKey");

                    b.Property<bool>("Deleted");

                    b.Property<long>("FirstCommitTimestamp");

                    b.Property<long>("LastCommitTimestamp");

                    b.Property<bool>("Listed");

                    b.HasKey("PackageKey");

                    b.HasIndex("LastCommitTimestamp");

                    b.ToTable("CatalogPackages");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogPageEntity", b =>
                {
                    b.Property<long>("CatalogPageKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("CatalogPageKey");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("CatalogPages");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CursorEntity", b =>
                {
                    b.Property<long>("CursorKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("Value");

                    b.HasKey("CursorKey");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cursors");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.ETagEntity", b =>
                {
                    b.Property<long>("ETagKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Value");

                    b.HasKey("ETagKey");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ETags");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageArchiveEntity", b =>
                {
                    b.Property<long>("PackageKey");

                    b.Property<uint>("CentralDirectorySize");

                    b.Property<byte[]>("Comment")
                        .IsRequired();

                    b.Property<ushort>("CommentSize");

                    b.Property<ushort>("DiskWithStartOfCentralDirectory");

                    b.Property<ushort>("EntriesForWholeCentralDirectory");

                    b.Property<ushort>("EntriesInThisDisk");

                    b.Property<int>("EntryCount");

                    b.Property<ushort>("NumberOfThisDisk");

                    b.Property<long>("OffsetAfterEndOfCentralDirectory");

                    b.Property<uint>("OffsetOfCentralDirectory");

                    b.Property<long>("Size");

                    b.Property<ulong?>("Zip64CentralDirectorySize");

                    b.Property<uint?>("Zip64DiskWithStartOfCentralDirectory");

                    b.Property<uint?>("Zip64DiskWithStartOfEndOfCentralDirectory");

                    b.Property<ulong?>("Zip64EndOfCentralDirectoryOffset");

                    b.Property<ulong?>("Zip64EntriesForWholeCentralDirectory");

                    b.Property<ulong?>("Zip64EntriesInThisDisk");

                    b.Property<uint?>("Zip64NumberOfThisDisk");

                    b.Property<long?>("Zip64OffsetAfterEndOfCentralDirectoryLocator");

                    b.Property<ulong?>("Zip64OffsetOfCentralDirectory");

                    b.Property<ulong?>("Zip64SizeOfCentralDirectoryRecord");

                    b.Property<uint?>("Zip64TotalNumberOfDisks");

                    b.Property<ushort?>("Zip64VersionMadeBy");

                    b.Property<ushort?>("Zip64VersionToExtract");

                    b.HasKey("PackageKey");

                    b.ToTable("PackageArchives");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageDownloadsEntity", b =>
                {
                    b.Property<long>("PackageKey");

                    b.Property<long>("Downloads");

                    b.HasKey("PackageKey");

                    b.ToTable("PackageDownloads");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageEntity", b =>
                {
                    b.Property<long>("PackageKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identity")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<long>("PackageRegistrationKey");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.HasKey("PackageKey");

                    b.HasIndex("Identity")
                        .IsUnique();

                    b.HasIndex("PackageRegistrationKey", "Version")
                        .IsUnique();

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageEntryEntity", b =>
                {
                    b.Property<long>("PackageEntryKey")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Comment")
                        .IsRequired();

                    b.Property<ushort>("CommentSize");

                    b.Property<uint>("CompressedSize");

                    b.Property<ushort>("CompressionMethod");

                    b.Property<uint>("Crc32");

                    b.Property<ushort>("DiskNumberStart");

                    b.Property<uint>("ExternalAttributes");

                    b.Property<byte[]>("ExtraField")
                        .IsRequired();

                    b.Property<ushort>("ExtraFieldSize");

                    b.Property<ushort>("Flags");

                    b.Property<ulong>("Index");

                    b.Property<ushort>("InternalAttributes");

                    b.Property<ushort>("LastModifiedDate");

                    b.Property<ushort>("LastModifiedTime");

                    b.Property<uint>("LocalHeaderOffset");

                    b.Property<byte[]>("Name")
                        .IsRequired();

                    b.Property<ushort>("NameSize");

                    b.Property<long>("PackageKey");

                    b.Property<uint>("UncompressedSize");

                    b.Property<ushort>("VersionMadeBy");

                    b.Property<ushort>("VersionToExtract");

                    b.HasKey("PackageEntryKey");

                    b.HasIndex("PackageKey", "Index")
                        .IsUnique();

                    b.ToTable("PackageEntries");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageQueryEntity", b =>
                {
                    b.Property<long>("PackageQueryKey")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CursorKey");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("PackageQueryKey");

                    b.HasIndex("CursorKey");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PackageQueries");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageQueryMatchEntity", b =>
                {
                    b.Property<long>("PackageQueryMatchKey")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PackageKey");

                    b.Property<long>("PackageQueryKey");

                    b.HasKey("PackageQueryMatchKey");

                    b.HasIndex("PackageKey");

                    b.HasIndex("PackageQueryKey", "PackageKey")
                        .IsUnique();

                    b.ToTable("PackageQueryMatches");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageRegistrationEntity", b =>
                {
                    b.Property<long>("PackageRegistrationKey")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.HasKey("PackageRegistrationKey");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("PackageRegistrations");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.V2PackageEntity", b =>
                {
                    b.Property<long>("PackageKey");

                    b.Property<long>("CreatedTimestamp");

                    b.Property<long?>("LastEditedTimestamp");

                    b.Property<long>("LastUpdatedTimestamp");

                    b.Property<bool>("Listed");

                    b.Property<long>("PublishedTimestamp");

                    b.HasKey("PackageKey");

                    b.HasIndex("CreatedTimestamp");

                    b.HasIndex("LastEditedTimestamp");

                    b.ToTable("V2Packages");
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogCommitEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.CatalogPageEntity", "CatalogPage")
                        .WithMany("CatalogCommits")
                        .HasForeignKey("CatalogPageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogLeafEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.CatalogCommitEntity", "CatalogCommit")
                        .WithMany("CatalogLeaves")
                        .HasForeignKey("CatalogCommitKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knapcode.ExplorePackages.Entities.CatalogPackageEntity", "CatalogPackage")
                        .WithMany("CatalogLeaves")
                        .HasForeignKey("PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.CatalogPackageEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageEntity", "Package")
                        .WithOne("CatalogPackage")
                        .HasForeignKey("Knapcode.ExplorePackages.Entities.CatalogPackageEntity", "PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageArchiveEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageEntity", "Package")
                        .WithOne("PackageArchive")
                        .HasForeignKey("Knapcode.ExplorePackages.Entities.PackageArchiveEntity", "PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageDownloadsEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageEntity", "Package")
                        .WithOne("PackageDownloads")
                        .HasForeignKey("Knapcode.ExplorePackages.Entities.PackageDownloadsEntity", "PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageRegistrationEntity", "PackageRegistration")
                        .WithMany("Packages")
                        .HasForeignKey("PackageRegistrationKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageEntryEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageArchiveEntity", "PackageArchive")
                        .WithMany("PackageEntries")
                        .HasForeignKey("PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageQueryEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.CursorEntity", "Cursor")
                        .WithMany()
                        .HasForeignKey("CursorKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.PackageQueryMatchEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageEntity", "Package")
                        .WithMany("PackageQueryMatches")
                        .HasForeignKey("PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageQueryEntity", "PackageQuery")
                        .WithMany()
                        .HasForeignKey("PackageQueryKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Knapcode.ExplorePackages.Entities.V2PackageEntity", b =>
                {
                    b.HasOne("Knapcode.ExplorePackages.Entities.PackageEntity", "Package")
                        .WithOne("V2Package")
                        .HasForeignKey("Knapcode.ExplorePackages.Entities.V2PackageEntity", "PackageKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
