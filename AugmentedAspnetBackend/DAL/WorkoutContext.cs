using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.DAL
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext() : base("WorkoutContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<WorkoutContext>());
        }

        public DbSet<Set> Sets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<ExerciseType> ExerciseTypes { get; set; }
        public DbSet<RoutineSet> RoutineSets { get; set; }

        public DbSet<CardioMachineExercise> CardioMachineExercises { get; set; }
        public DbSet<CaffeineNutrientIntake> CaffeineNutrientIntakes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}