using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.ViewModels.Workouts
{
    public class SetViewModel
    {
        public SetViewModel() { }
        public SetViewModel(Set set)
        {
            Reps = set.Reps;
            Seconds = set.Seconds;
            Weight = set.Weight;
            Distance = set.Distance;
            Comment = set.Comment;
        }
        public int Reps { get; set; } 
        public int Seconds { get; set; }
        public Double Weight { get; set; }
        public Double Distance { get; set; }
        public String Comment { get; set; }
    }
}