using ExerciseService.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Repositories
{
    public class ExerciseRepository
    {
        private ExerciseDbContext dbContext;
        public ExerciseRepository(ExerciseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ExercisePage GetExercises(string q, int pageNumber, int pageSize, Guid userId)
        {
            int totalElements;
            if (q == null) q = "";

            totalElements = dbContext.Exercises
                .Where(exercise => exercise.title.Contains(q) || exercise.description.Contains(q))
                .Count();

            int totalPages = totalElements / pageSize;

            List<GenericExerciseDataModel> exercises =
                dbContext.Exercises
                    .Where(exercise => exercise.title.Contains(q) || exercise.description.Contains(q))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(x => FillOutLikeData(x, userId))
                    .ToList();

            return new ExercisePage()
            {
                Items = exercises,
                TotalPages = totalElements % pageSize == 0 ? totalPages : totalPages + 1,
                TotalElements = totalElements
            };
        }

        public ExercisePage GetExercisesByUser(Guid id, int pageNumber, int pageSize)
        {
            int totalElements = dbContext.Exercises
                .Where(exercise => exercise.UserId.Equals(id))
                .Count();
            int totalPages = totalElements / pageSize;

            return new ExercisePage()
            {
                Items = dbContext.Exercises
                    .Where(exercise => exercise.UserId.Equals(id))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                TotalPages = totalElements % pageSize == 0 ? totalPages : totalPages + 1,
                TotalElements = totalElements
            };
        }
        public ExercisePage GetExercisesLikedByUser(Guid id, int pageNumber, int pageSize)
        {
            int totalElements = dbContext.Exercises
                .Where(exercise => dbContext.Likes
                    .Where(like => like.UserId.Equals(id))
                    .Any(like => like.ExerciseId.Equals(exercise.Id)))
                .Count();
            int totalPages = totalElements / pageSize;

            return new ExercisePage()
            {
                Items = dbContext.Exercises
                    .Where(exercise => dbContext.Likes
                        .Where(like => like.UserId.Equals(id))
                        .Any(like => like.ExerciseId.Equals(exercise.Id)))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList(),
                TotalPages = totalElements % pageSize == 0 ? totalPages : totalPages + 1,
                TotalElements = totalElements
            };
        }

        public GenericExerciseDataModel CreateExercise(GenericExerciseDataModel exercise)
        {
            EntityEntry<GenericExerciseDataModel> entityEntry = dbContext.Exercises.Add(exercise);
            dbContext.SaveChanges();
            return entityEntry.Entity;
        }

        public GenericExerciseDataModel UpdateExercise(GenericExerciseDataModel exercise)
        {
            EntityEntry<GenericExerciseDataModel> entityEntry = dbContext.Exercises.Update(exercise);
            dbContext.SaveChanges();
            return entityEntry.Entity;
        }

        public void DeleteExercise(Guid id)
        {
            dbContext.Remove(dbContext.Exercises.Single(exercise => exercise.Id.Equals(id)));
            dbContext.SaveChanges();
        }

        public void LikeExercise(LikeDBO like)
        {
            dbContext.Likes.Add(like);
            dbContext.SaveChanges();
        }

        public void UnlikeExercise(Guid id, Guid userId)
        {
            dbContext.Likes
                .Where(like => like.ExerciseId.Equals(id) && like.UserId.Equals(userId))
                .ToList().ForEach(like => dbContext.Remove(like));
            dbContext.SaveChanges();
        }

        public GenericExerciseDataModel GetExercise(Guid id)
        {
            return dbContext.Exercises.Find(id);
        }

        private GenericExerciseDataModel FillOutLikeData(GenericExerciseDataModel exercise, Guid userId)
        {
            exercise.AmountOfLikes = dbContext.Likes.Where(like => like.ExerciseId.Equals(exercise.Id)).Count();
            exercise.IsLiked = dbContext.Likes.Any(like => like.UserId.Equals(userId) && like.ExerciseId.Equals(exercise.Id));
            return exercise;
        }
    }
}
