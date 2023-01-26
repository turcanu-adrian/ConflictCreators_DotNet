﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Games.Elements.Prompt", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CorrectAnswer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromptSetId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WrongAnswers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromptSetId");

                    b.ToTable("Prompts");

                    b.HasData(
                        new
                        {
                            Id = "b9bf8ab2c8d04ea680921ccfeff9a0ac",
                            CorrectAnswer = "Yellow",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What color is the sun?",
                            WrongAnswers = "[\"Green\",\"Red\",\"Blue\",\"Orange\"]"
                        },
                        new
                        {
                            Id = "25fce8d270c74d0586a2c9ff32f76920",
                            CorrectAnswer = "4",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "How many legs does a sheep have?",
                            WrongAnswers = "[\"2\",\"3\",\"4\"]"
                        },
                        new
                        {
                            Id = "c02bbe8b4ef24b01a6a92609da732bb9",
                            CorrectAnswer = "Paris",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What is the capital of France?",
                            WrongAnswers = "[\"Bucharest\",\"Rome\",\"Moscow\"]"
                        },
                        new
                        {
                            Id = "c826b8560f384f898b709bd5082d1c83",
                            CorrectAnswer = "January",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What month has the internship ended in?",
                            WrongAnswers = "[\"December\",\"October\",\"February\"]"
                        },
                        new
                        {
                            Id = "024e9f4d402144aabf670c948eec49f0",
                            CorrectAnswer = "Bucharest",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What is the capital of Romania?",
                            WrongAnswers = "[\"Paris\",\"Rome\",\"Texas\"]"
                        },
                        new
                        {
                            Id = "320f2aabb05747f89e7e741585652772",
                            CorrectAnswer = "1000 grams",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "How many grams makes 1 kilogram?",
                            WrongAnswers = "[\"10 grams\",\"100 grams\",\"1 gram\"]"
                        },
                        new
                        {
                            Id = "d01f6e51a95949d787dbdc0e9c1e01a3",
                            CorrectAnswer = "50",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "How many states are in the United States?",
                            WrongAnswers = "[\"40\",\"45\",\"52\"]"
                        },
                        new
                        {
                            Id = "e8d55525ab644652910b2ac03a5bf06b",
                            CorrectAnswer = "Bald Eagle",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What is the National Animal of the USA?",
                            WrongAnswers = "[\"Black Bear\",\"Grizzly Bear\",\"American Bison\"]"
                        },
                        new
                        {
                            Id = "0d15de8340594103977491d0a22bf94f",
                            CorrectAnswer = "Clownfish",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "Which type of fish is Nemo?",
                            WrongAnswers = "[\"Swordfish\",\"Sailfish\",\"Whale\"]"
                        },
                        new
                        {
                            Id = "bfddf20ebe974a77973a2ad920dc042c",
                            CorrectAnswer = "Asia",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "Which is the largest continent?",
                            WrongAnswers = "[\"Europe\",\"North America\",\"Africa\"]"
                        },
                        new
                        {
                            Id = "2bb7a830c400473ab940d4331b0e6321",
                            CorrectAnswer = "Chiuaua",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What is the smallest breed of dog?",
                            WrongAnswers = "[\"Golden Retriever\",\"Bulldog\",\"Pitbull\"]"
                        },
                        new
                        {
                            Id = "0bface1bcae847a6826f6a8e408de9fa",
                            CorrectAnswer = "8",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "How many sides does an Octagon have?",
                            WrongAnswers = "[\"4\",\"6\",\"10\"]"
                        },
                        new
                        {
                            Id = "e97187ab96a4434e8b2df87372c7b9a7",
                            CorrectAnswer = "Parrot",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "Which bird can mimic humans?",
                            WrongAnswers = "[\"Eagle\",\"Seagull\",\"Pelican\"]"
                        },
                        new
                        {
                            Id = "ed1d641e06c64286a47aff6038340f0b",
                            CorrectAnswer = "Honey",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What do bees produce?",
                            WrongAnswers = "[\"Milk\",\"Flowers\",\"Water\"]"
                        },
                        new
                        {
                            Id = "ae99acb2348a4be49fae3e19198f1e56",
                            CorrectAnswer = "Green",
                            PromptSetId = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            Question = "What color is an emerald?",
                            WrongAnswers = "[\"Golden\",\"Black\",\"Pink\"]"
                        });
                });

            modelBuilder.Entity("Domain.Games.Elements.PromptSet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PromptSets");

                    b.HasData(
                        new
                        {
                            Id = "fdd1fd6f2aa24d29a1a6fb710df9f033",
                            CreatedByUserId = "default",
                            Name = "Default Prompt Set",
                            Tags = "[\"default\",\"basic\"]"
                        });
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("AchievementPoints")
                        .HasColumnType("int");

                    b.Property<string>("Badges")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("FastestRun")
                        .HasColumnType("time");

                    b.Property<int>("GamesWon")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("currentAvatar")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Games.Elements.Prompt", b =>
                {
                    b.HasOne("Domain.Games.Elements.PromptSet", null)
                        .WithMany("Prompts")
                        .HasForeignKey("PromptSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Games.Elements.PromptSet", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany("FavouritePromptSets")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Games.Elements.PromptSet", b =>
                {
                    b.Navigation("Prompts");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("FavouritePromptSets");
                });
#pragma warning restore 612, 618
        }
    }
}
