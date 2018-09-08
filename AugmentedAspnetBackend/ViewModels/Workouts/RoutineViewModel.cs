using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.ViewModels.Workouts
{
    public class RoutineViewModel
    {
        public RoutineViewModel() { }

        public RoutineViewModel(Routine routine)
        {
            Name = routine.Name;
            Comment = routine.Comment;
        }

        public String Name { get; set; }
        public String Comment { get; set; }
        public ExerciseViewModel Exercise { get; set; }
        public IEnumerable<SetViewModel> Sets { get; set; }
    }
}