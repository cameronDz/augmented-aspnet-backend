using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace augmented_aspnet_backend.Models.Workout
{
    public class Exercise
    {
        [Key]
        [Display(Name = "Exercise Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExerciseId { get; set; }
        [Display(Name = "Exercise Name")]
        public string Name { get; set; }
        [Display(Name = "Exercise Type")]
        public string Type { get; set; }
        [Display(Name = "Exercise Description")]
        public string Description { get; set; }
    }
}