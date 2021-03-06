// <auto-generated />
using System;
using ExerciseService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExerciseService.Migrations
{
    [DbContext(typeof(ExerciseDbContext))]
    partial class ExerciseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ExerciseService.Models.GenericExerciseDataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Exercises");

                    b.HasDiscriminator<string>("Discriminator").HasValue("GenericExerciseDataModel");
                });

            modelBuilder.Entity("ExerciseService.Models.LikeDBO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ExerciseId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("ExerciseService.Models.IntervalRecognitionExerciseDataModel", b =>
                {
                    b.HasBaseType("ExerciseService.Models.GenericExerciseDataModel");

                    b.Property<string>("InternalData")
                        .HasColumnType("longtext");

                    b.Property<int>("ascentionType")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("IntervalRecognitionExerciseDataModel");
                });

            modelBuilder.Entity("ExerciseService.Models.NoteExerciseDataModel", b =>
                {
                    b.HasBaseType("ExerciseService.Models.GenericExerciseDataModel");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("NoteExerciseDataModel");
                });

            modelBuilder.Entity("ExerciseService.Models.PlayAlongExerciseDataModel", b =>
                {
                    b.HasBaseType("ExerciseService.Models.GenericExerciseDataModel");

                    b.Property<int>("testValue")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("PlayAlongExerciseDataModel");
                });
#pragma warning restore 612, 618
        }
    }
}
