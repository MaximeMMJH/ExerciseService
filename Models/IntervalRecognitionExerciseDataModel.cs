using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Models
{
    public class IntervalRecognitionExerciseDataModel : GenericExerciseDataModel
    {
        public AscentionType ascentionType { get; set; }
        public string InternalData { get; set; }
        [NotMapped]
        public int[] Intervals
        {
            get
            {
                return Array.ConvertAll(InternalData.Split(';'), Int32.Parse);
            }
            set
            {
                InternalData = String.Join(";", value.Select(p => p.ToString()).ToArray());
            }
        }
    }
}
