using ExerciseService.Models;
using ExerciseService.Models.JsonModelPropertyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Transformers
{
    public class ExerciseTransformer
    {
        public static ExercisePageResponse ToExercisePageReponse(ExercisePage page)
        {
            return new ExercisePageResponse()
            {
                Items = ToJsonModels(page.Items),
                TotalElements = page.TotalElements,
                TotalPages = page.TotalPages
            };
        }

        public static ExerciseJsonModel ToJsonModel(GenericExerciseDataModel exerciseDataModel)
        {
            ExerciseJsonModel jsonModel = new ExerciseJsonModel()
            {
                Id = exerciseDataModel.Id,
                UserId = exerciseDataModel.UserId,
                title = exerciseDataModel.title,
                description = exerciseDataModel.description,
                AmountOfLikes = exerciseDataModel.AmountOfLikes,
                IsLiked = exerciseDataModel.IsLiked
            };

            if (exerciseDataModel is IntervalRecognitionExerciseDataModel)
            {
                IntervalRecognitionExerciseDataModel derivedModel = (IntervalRecognitionExerciseDataModel)exerciseDataModel;
                jsonModel.exerciseType = ExerciseType.IntervalRecognition;

                jsonModel.intervalRecognitionExerciseProperties = new IntervalRecognitionExerciseProperties()
                {
                    ascentionType = derivedModel.ascentionType,
                    Intervals = derivedModel.Intervals
                };
            } else if (exerciseDataModel is PlayAlongExerciseDataModel)
            {
                PlayAlongExerciseDataModel derivedModel = (PlayAlongExerciseDataModel)exerciseDataModel;
                jsonModel.exerciseType = ExerciseType.PlayAlong;

                jsonModel.playAlongExerciseProperties = new PlayAlongExerciseProperties()
                {
                    testValue = derivedModel.testValue
                };
            } else if (exerciseDataModel is NoteExerciseDataModel)
            {
                NoteExerciseDataModel derivedModel = (NoteExerciseDataModel)exerciseDataModel;
                jsonModel.exerciseType = ExerciseType.Note;

                jsonModel.NoteExerciseProperties = new NoteExerciseProperties()
                {
                    Content = derivedModel.content
                };
            } else
            {
                throw new Exception("ToJsonModel: impossile model type" + exerciseDataModel.GetType().ToString());
            }

            return jsonModel;
        }

        public static GenericExerciseDataModel ToDataModel(ExerciseJsonModel exerciseJsonModel)
        {
            if (exerciseJsonModel.exerciseType.Equals(ExerciseType.IntervalRecognition))
            {
                if (exerciseJsonModel.intervalRecognitionExerciseProperties is null)
                {
                    throw new Exception("ToDataModel intervalRecognitionExerciseProperties is required"); 
                }
                else
                {
                    return new IntervalRecognitionExerciseDataModel()
                    {
                        Id = exerciseJsonModel.Id,
                        UserId = exerciseJsonModel.UserId,
                        title = exerciseJsonModel.title,
                        description = exerciseJsonModel.description,
                        ascentionType = exerciseJsonModel.intervalRecognitionExerciseProperties.ascentionType,
                        Intervals = exerciseJsonModel.intervalRecognitionExerciseProperties.Intervals,
                        AmountOfLikes = exerciseJsonModel.AmountOfLikes,
                        IsLiked = exerciseJsonModel.IsLiked
                    };
                }
            } else if (exerciseJsonModel.exerciseType.Equals(ExerciseType.PlayAlong))
            {
                if (exerciseJsonModel.playAlongExerciseProperties is null)
                {
                    throw new Exception("ToDataModel playAlongExerciseProperties is required");
                } else
                {
                    return new PlayAlongExerciseDataModel()
                    {
                        Id = exerciseJsonModel.Id,
                        UserId = exerciseJsonModel.UserId,
                        title = exerciseJsonModel.title,
                        description = exerciseJsonModel.description,
                        testValue = exerciseJsonModel.playAlongExerciseProperties.testValue,
                        AmountOfLikes = exerciseJsonModel.AmountOfLikes,
                        IsLiked = exerciseJsonModel.IsLiked
                    };
                }
            } else if (exerciseJsonModel.exerciseType.Equals(ExerciseType.Note))
            {
                if (exerciseJsonModel.NoteExerciseProperties is null)
                {
                    throw new Exception("ToDataModel NoteExerciseProperties is required");
                }
                else
                {
                    return new NoteExerciseDataModel()
                    {
                        Id = exerciseJsonModel.Id,
                        UserId = exerciseJsonModel.UserId,
                        title = exerciseJsonModel.title,
                        description = exerciseJsonModel.description,
                        content = exerciseJsonModel.NoteExerciseProperties.Content,
                        AmountOfLikes = exerciseJsonModel.AmountOfLikes,
                        IsLiked = exerciseJsonModel.IsLiked
                    };
                }
            } else
            {
                throw new Exception();
            }
        }

        public static List<ExerciseJsonModel> ToJsonModels(List<GenericExerciseDataModel> exerciseDataModels)
        {
            return exerciseDataModels.Select(userModel => ToJsonModel(userModel)).ToList();
        }

        public static List<GenericExerciseDataModel> ToDataModels(List<ExerciseJsonModel> exerciseJsonModels)
        {
            return exerciseJsonModels.Select(jsonModel => ToDataModel(jsonModel)).ToList();
        }
    }
}
