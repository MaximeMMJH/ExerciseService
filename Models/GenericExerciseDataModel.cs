using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseService.Models
{
    public class GenericExerciseDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        [NotMapped]
        public bool IsLiked { get; set; }
        [NotMapped]
        public int AmountOfLikes { get; set; }
    }
}
