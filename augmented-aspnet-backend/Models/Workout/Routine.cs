using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace augmented_aspnet_backend.Models.Workout
{
    public class Routine
    {
        [Key]
        [Display(Name = "Routine Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoutineId { get; set; }
        [Display(Name = "Session Id")]
        public int SessionId { get; set; }
        [Display(Name = "Exercise Id")]
        public int ExerciseId { get; set; }
        [Display(Name = "Routine Comment")]
        public string Comment { get; set; }
    }
}