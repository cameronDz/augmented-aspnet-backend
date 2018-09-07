using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.Workout
{
    public class ExerciseType
    {
        [Key]
        [Display(Name = "Exercise Type Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExercisTypeId { get; set; }
        [Display(Name = "Exercise Type Name")]
        public string Name { get; set; }
    }
}