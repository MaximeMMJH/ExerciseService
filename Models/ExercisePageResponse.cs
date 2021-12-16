using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Models
{
    public class ExercisePageResponse
    {
        public List<ExerciseJsonModel> Items { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
    }
}
