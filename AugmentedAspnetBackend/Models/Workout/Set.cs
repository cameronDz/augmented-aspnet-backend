using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.Workout
{
    public class Set
    {
        [Key]
        [Display(Name = "Set Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SetId { get; set; }
        [Display(Name = "Set Reps")]
        public int Reps { get; set; }
        [Display(Name = "Set Seconds")]
        public int Seconds { get; set; }
        [Display(Name = "Set Weight")]
        public double Weight { get; set; }
        [Display(Name = "Set Comment")]
        public string Comment { get; set; }
    }
}