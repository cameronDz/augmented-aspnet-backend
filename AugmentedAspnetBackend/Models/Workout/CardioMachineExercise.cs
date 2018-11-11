using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.Workout
{
    public class CardioMachineExercise
    {
        [Key]
        [Display(Name = "Cardio Machine Exercise Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CardioMachineExerciseId { get; set; }
        [Display(Name = "Machine Type")]
        public String MachineType { get; set; }
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Duration in Seconds")]
        public int DurationSeconds { get; set; }
        [Display(Name = "Distance in Miles")]
        public double DistanceMiles { get; set; }
        [Display(Name = "User Name")]
        public String UserName { get; set; }
        [Display(Name = "Comment")]
        public String Comment { get; set; }
    }
}