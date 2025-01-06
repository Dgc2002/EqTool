﻿// <auto-generated />
using System;
using EQToolApis.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EQToolApis.Migrations
{
    [DbContext(typeof(EQToolContext))]
    [Migration("20250104171041_IndexAddOnnotableNPCTable")]
    partial class IndexAddOnnotableNPCTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EQToolApis.DB.Models.APILog", b =>
                {
                    b.Property<int>("APILogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("APILogId"));

                    b.Property<short>("APIAction")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<string>("LogMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("APILogId");

                    b.ToTable("APILogs");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQAuctionPlayerV2", b =>
                {
                    b.Property<int>("EQAuctionPlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EQAuctionPlayerId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.HasKey("EQAuctionPlayerId");

                    b.HasIndex("Server");

                    b.ToTable("EQAuctionPlayersV2");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQDeath", b =>
                {
                    b.Property<int>("EQDeathId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EQDeathId"));

                    b.Property<DateTimeOffset>("EQDeathTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EQZoneId")
                        .HasColumnType("int");

                    b.Property<double?>("LocX")
                        .HasColumnType("float");

                    b.Property<double?>("LocY")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.HasKey("EQDeathId");

                    b.HasIndex("EQZoneId");

                    b.ToTable("EQDeaths");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQNotableActivity", b =>
                {
                    b.Property<int>("EQNotableActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EQNotableActivityId"));

                    b.Property<DateTimeOffset>("ActivityTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("EQNotableNPCId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeath")
                        .HasColumnType("bit");

                    b.Property<double?>("LocX")
                        .HasColumnType("float");

                    b.Property<double?>("LocY")
                        .HasColumnType("float");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.HasKey("EQNotableActivityId");

                    b.HasIndex("ActivityTime");

                    b.HasIndex("EQNotableNPCId");

                    b.HasIndex("Server", "IsDeath", "ActivityTime");

                    b.ToTable("EQNotableActivities");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQNotableNPC", b =>
                {
                    b.Property<int>("EQNotableNPCId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EQNotableNPCId"));

                    b.Property<int>("EQZoneId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("EQNotableNPCId");

                    b.HasIndex("EQZoneId");

                    b.ToTable("EQNotableNPCs");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQTunnelAuctionItemV2", b =>
                {
                    b.Property<long>("EQTunnelAuctionItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EQTunnelAuctionItemId"));

                    b.Property<int?>("AuctionPrice")
                        .HasColumnType("int");

                    b.Property<long>("EQTunnelMessageId")
                        .HasColumnType("bigint");

                    b.Property<int>("EQitemId")
                        .HasColumnType("int");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.HasKey("EQTunnelAuctionItemId");

                    b.HasIndex("EQTunnelMessageId");

                    b.HasIndex("EQitemId");

                    b.ToTable("EQTunnelAuctionItemsV2");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQTunnelMessageV2", b =>
                {
                    b.Property<long>("EQTunnelMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("EQTunnelMessageId"));

                    b.Property<byte>("AuctionType")
                        .HasColumnType("tinyint");

                    b.Property<long>("DiscordMessageId")
                        .HasColumnType("bigint");

                    b.Property<int>("EQAuctionPlayerId")
                        .HasColumnType("int");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.Property<DateTimeOffset>("TunnelTimestamp")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("EQTunnelMessageId");

                    b.HasIndex("AuctionType");

                    b.HasIndex("DiscordMessageId");

                    b.HasIndex("EQAuctionPlayerId");

                    b.HasIndex("Server");

                    b.HasIndex("TunnelTimestamp");

                    b.HasIndex("Server", "AuctionType");

                    b.HasIndex("TunnelTimestamp", "AuctionType");

                    b.ToTable("EQTunnelMessagesV2");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQZone", b =>
                {
                    b.Property<int>("EQZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EQZoneId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(48)
                        .HasColumnType("nvarchar(48)");

                    b.HasKey("EQZoneId");

                    b.ToTable("EQZones");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQitemV2", b =>
                {
                    b.Property<int>("EQitemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EQitemId"));

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTimeOffset?>("LastWTBSeen")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastWTSSeen")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.Property<int>("TotalWTBAuctionAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBAuctionCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast30DaysAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast30DaysCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast60DaysAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast60DaysCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast6MonthsAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast6MonthsCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast90DaysAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLast90DaysCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLastYearAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTBLastYearCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSAuctionAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSAuctionCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast30DaysAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast30DaysCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast60DaysAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast60DaysCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast6MonthsAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast6MonthsCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast90DaysAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLast90DaysCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLastYearAverage")
                        .HasColumnType("int");

                    b.Property<int>("TotalWTSLastYearCount")
                        .HasColumnType("int");

                    b.HasKey("EQitemId");

                    b.HasIndex("Server");

                    b.HasIndex("ItemName", "Server");

                    b.ToTable("EQitemsV2");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EqToolException", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BuildType")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EventType")
                        .HasColumnType("int");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<byte?>("Server")
                        .HasColumnType("tinyint");

                    b.Property<string>("Version")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("EqToolExceptions");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.IPBan", b =>
                {
                    b.Property<string>("IpAddress")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("IpAddress");

                    b.ToTable("IPBans");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.Player", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.Property<string>("GuildName")
                        .HasMaxLength(48)
                        .HasColumnType("nvarchar(48)");

                    b.Property<byte?>("Level")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("PlayerClass")
                        .HasColumnType("tinyint");

                    b.HasKey("Name", "Server");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.QuakeTime", b =>
                {
                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte>("Server")
                        .HasColumnType("tinyint");

                    b.HasKey("DateTime");

                    b.ToTable("QuakeTimes");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.ServerMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AlertType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ServerMessages");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQDeath", b =>
                {
                    b.HasOne("EQToolApis.DB.Models.EQZone", "EQZone")
                        .WithMany("EQDeaths")
                        .HasForeignKey("EQZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EQZone");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQNotableActivity", b =>
                {
                    b.HasOne("EQToolApis.DB.Models.EQNotableNPC", "EQNotableNPC")
                        .WithMany("EQNotableActivities")
                        .HasForeignKey("EQNotableNPCId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EQNotableNPC");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQNotableNPC", b =>
                {
                    b.HasOne("EQToolApis.DB.Models.EQZone", "EQZone")
                        .WithMany()
                        .HasForeignKey("EQZoneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EQZone");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQTunnelAuctionItemV2", b =>
                {
                    b.HasOne("EQToolApis.DB.Models.EQTunnelMessageV2", "EQTunnelMessage")
                        .WithMany("EQTunnelAuctionItems")
                        .HasForeignKey("EQTunnelMessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EQToolApis.DB.Models.EQitemV2", "EQitem")
                        .WithMany("EQTunnelAuctionItems")
                        .HasForeignKey("EQitemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EQTunnelMessage");

                    b.Navigation("EQitem");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQTunnelMessageV2", b =>
                {
                    b.HasOne("EQToolApis.DB.Models.EQAuctionPlayerV2", "EQAuctionPlayer")
                        .WithMany()
                        .HasForeignKey("EQAuctionPlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EQAuctionPlayer");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQNotableNPC", b =>
                {
                    b.Navigation("EQNotableActivities");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQTunnelMessageV2", b =>
                {
                    b.Navigation("EQTunnelAuctionItems");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQZone", b =>
                {
                    b.Navigation("EQDeaths");
                });

            modelBuilder.Entity("EQToolApis.DB.Models.EQitemV2", b =>
                {
                    b.Navigation("EQTunnelAuctionItems");
                });
#pragma warning restore 612, 618
        }
    }
}
