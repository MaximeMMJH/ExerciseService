using ExerciseService.Models;
using ExerciseService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Logic
{
    public class ExerciseFacade
    {
        private ExerciseRepository repository;

        public ExerciseFacade(ExerciseRepository exerciseRepository)
        {
            repository = exerciseRepository;
        }
        internal ExercisePage GetExercises(string q, int pageNumber, int pageSize, Guid userId)
        {
            return repository.GetExercises(q, pageNumber, pageSize, userId);
        }

        internal ExercisePage GetExercisesByUser(Guid id, int pageNumber, int pageSize)
        {
            return repository.GetExercisesByUser(id, pageNumber, pageSize);
        }

        internal GenericExerciseDataModel GetExercise(Guid id)
        {
            return repository.GetExercise(id);
        }

        internal GenericExerciseDataModel CreateExercise(GenericExerciseDataModel exercise)
        {
            return repository.CreateExercise(exercise);
        }

        internal void DeleteExercise(Guid id)
        {
            repository.DeleteExercise(id);
        }

        internal GenericExerciseDataModel UpdateExercise(GenericExerciseDataModel exercise)
        {
            return repository.UpdateExercise(exercise);
        }

        public void LikeExercise(LikeDBO like)
        {
            repository.LikeExercise(like);
        }

        public void UnlikeExercise(Guid id, Guid userId)
        {
            repository.UnlikeExercise(id, userId);
        }

        internal ExercisePage GetExercisesLikedByUser(Guid id, int pageNumber, int pageSize)
        {
            return repository.GetExercisesLikedByUser(id, pageNumber, pageSize);
        }
    }
}
