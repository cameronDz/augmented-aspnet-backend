using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Models.Workout
{
    public class RoutineSet
    {
        [Key]
        [Display(Name = "Routine Set Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoutineSetId { get; set; }
        [Display(Name = "Routine Id")]
        public int RoutineId { get; set; }
        [Display(Name = "Set Id")]
        public int SetId { get; set; }
    }
}