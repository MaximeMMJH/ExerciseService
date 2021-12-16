using ExerciseService.Models.JsonModelPropertyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Models
{
    public class ExerciseJsonModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int AmountOfLikes { get; set; }
        public bool IsLiked { get; set; }
        public IntervalRecognitionExerciseProperties intervalRecognitionExerciseProperties { get; set; }
        public PlayAlongExerciseProperties playAlongExerciseProperties { get; set; }
        public NoteExerciseProperties NoteExerciseProperties { get; set; }
        public ExerciseType exerciseType { get; set; }
    }
}
