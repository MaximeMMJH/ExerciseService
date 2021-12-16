using ExerciseService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Repositories
{
    public class ExerciseDbContext : DbContext
    {
        public ExerciseDbContext(DbContextOptions<ExerciseDbContext> options) :base(options)
        {

        }

        public DbSet<GenericExerciseDataModel> Exercises { get; set; }
        public DbSet<IntervalRecognitionExerciseDataModel> IntervalRecognitionExercises { get; set; }
        public DbSet<PlayAlongExerciseDataModel> PlayAlongExercises { get; set; }
        public DbSet<NoteExerciseDataModel> NoteExercises { get; set; }
        public DbSet<LikeDBO> Likes { get; set; }
    }
}
