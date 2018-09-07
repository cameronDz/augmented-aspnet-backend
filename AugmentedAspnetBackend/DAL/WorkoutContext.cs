using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.DAL
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext() : base("WorkoutContext")
        {
        }

        public DbSet<Set> Sets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Routine> Rountines { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<RoutineSet> RoutineSets { get; set; }
    }
}