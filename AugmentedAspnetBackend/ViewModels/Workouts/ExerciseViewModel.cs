using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.ViewModels.Workouts
{
    public class ExerciseViewModel
    {
        public ExerciseViewModel() { }

        public ExerciseViewModel(Exercise exercise)
        {
            Name = exercise.Name;
            Description = exercise.Description;
        }

        public String Name { get; set; }
        public String Type { get; set; }
        public String Description { get; set; }
    }
}