using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Models.JsonModelPropertyModels
{
    public class IntervalRecognitionExerciseProperties
    {
        public AscentionType ascentionType { get; set; }
        public int[] Intervals { get; set; }
    }
}
