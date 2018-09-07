using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.Workout
{
    public class Exercise
    {
        [Key]
        [Display(Name = "Exercise Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExerciseId { get; set; }
        [Display(Name = "Exercise Type Id")]
        public int TypeId { get; set; }
        [Display(Name = "Exercise Name")]
        public string Name { get; set; }
        [Display(Name = "Exercise Description")]
        public string Description { get; set; }
    }
}