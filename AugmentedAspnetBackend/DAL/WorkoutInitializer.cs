using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.DAL
{
    public class WorkoutInitializer : DropCreateDatabaseIfModelChanges<WorkoutContext>
    {
        protected override void Seed(WorkoutContext context)
        {
            var sessions = CreateSessionsData();
            sessions.ForEach(s => context.Sessions.Add(s));
            context.SaveChanges();

            var routines = CreateRoutineData();
            routines.ForEach(s => context.Routines.Add(s));
            context.SaveChanges();

            var exercises = CreateExerciseData();
            exercises.ForEach(s => context.Exercises.Add(s));
            context.SaveChanges();

            var exerciseTypese = CreateExerciseTypeData();
            exerciseTypese.ForEach(s => context.ExerciseTypes.Add(s));
            context.SaveChanges();

            var sets = CreateSetData();
            sets.ForEach(s => context.Sets.Add(s));
            context.SaveChanges();

            var routineSets = CreateRoutineSet();
            routineSets.ForEach(s => context.RoutineSets.Add(s));
            context.SaveChanges();
        }

        private List<Session> CreateSessionsData()
        {
            var sessions = new List<Session>
            {
                new Session { Name = "Monday Morning 9/3/2018", Comment = "Good work out.", StartTime = new DateTime(2018, 09, 03, 07, 30, 00), EndTime = new DateTime(2018, 09, 03, 09, 00, 00) },
                new Session { Name = "Tuesday Morning 9/4/2018", Comment = "Solid work out. Late morning.", StartTime = new DateTime(2018, 09, 04, 10, 30, 00), EndTime = new DateTime(2018, 09, 04, 12, 00, 00) }
            };
            return sessions;
        }

        private List<Routine> CreateRoutineData()
        {
            var routines = new List<Routine>
            {
                new Routine { Name = "Barbell Bench Press 5 3 1 Week 1", Comment = "Good pump.", ExerciseId = 1, SessionId = 1 },
                new Routine { Name = "Snatch Barbell Low Pull", Comment = "Great pulls.", ExerciseId = 2, SessionId = 1 },
                new Routine { Name = "Dumbbell Standing Hammer Curls", Comment = "Good pump, looking big.", ExerciseId = 3, SessionId = 1 },
                new Routine { Name = "Two-mile Treadmill Jog.", Comment = "Good sweat.", ExerciseId = 4, SessionId = 1 },

                new Routine { Name = "Barbell Front Squat 5 3 1 Week 1", Comment = "Got deep.", ExerciseId = 5, SessionId = 2 },
                new Routine { Name = "Full Barbell Clean", Comment = "Good pulls, good catches.", ExerciseId = 6, SessionId = 2 },
                new Routine { Name = "Barbell Drag Curls", Comment = "Good pump, looking big.", ExerciseId = 7, SessionId = 2 },
                new Routine { Name = "One-mile Treadmill Run.", Comment = "Good sweat.", ExerciseId = 4, SessionId = 2 }
            };
            return routines;
        }

        private List<Exercise> CreateExerciseData()
        {
            var exercises = new List<Exercise>
            {
                new Exercise { Name = "Barbell Bench Press", Description = "Standard Barbell Benchpress.", TypeId = 1 },
                new Exercise { Name = "Snatch Barbell Low Pull", Description = "Lower pull for Olympic Barbell Snatch.", TypeId = 1 },
                new Exercise { Name = "Dumbbell Standing Hammer Curl", Description = "Standing, synchronized dumbbell hammer curls.", TypeId = 2 },
                new Exercise { Name = "Treadmill", Description = "Indoor treadmill.", TypeId = 3 },
                new Exercise { Name = "Barbell Front Squat", Description = "Standard Barbell Front Squat.", TypeId = 1 },
                new Exercise { Name = "Full Barbell Clean", Description = "Full olympic barbell clean.", TypeId = 1 },
                new Exercise { Name = "Barbell Drag Curls", Description = "Standard Barbell Curls.", TypeId = 1 }
             };
            return exercises;
        }

        private List<ExerciseType> CreateExerciseTypeData()
        {
            var exerciseTypese = new List<ExerciseType>
            {
                new ExerciseType { Name = "Barbell Lift" },
                new ExerciseType { Name = "Dumbbell Lift" },
                new ExerciseType { Name = "Cardio" }
            };
            return exerciseTypese;
        }

        private List<Set> CreateSetData()
        {
            var sets = new List<Set>
            {
                new Set { Weight = 135, Reps = 5 },
                new Set { Weight = 155, Reps = 5 },
                new Set { Weight = 185, Reps = 5 },

                new Set { Weight = 185, Reps = 3 },
                new Set { Weight = 205, Reps = 3 },
                new Set { Weight = 255, Reps = 3 },

                new Set { Weight = 35, Reps = 8 },
                new Set { Weight = 40, Reps = 8 },
                new Set { Weight = 45, Reps = 8 },

                new Set { Seconds = 1200, Distance = 2 },

                new Set { Weight = 135, Reps = 5 },
                new Set { Weight = 155, Reps = 5 },
                new Set { Weight = 185, Reps = 5 },


                new Set { Weight = 135, Reps = 3 },
                new Set { Weight = 135, Reps = 3 },
                new Set { Weight = 135, Reps = 3 },

                new Set { Weight = 80, Reps = 8 },
                new Set { Weight = 80, Reps = 8 },
                new Set { Weight = 80, Reps = 8 },

                new Set { Seconds = 330, Distance = 1 }
            };
            return sets;
        }

        private List<RoutineSet> CreateRoutineSet()
        {
            var routineSets = new List<RoutineSet>
            {
                new RoutineSet { SetId = 1, RoutineId = 1 },
                new RoutineSet { SetId = 2, RoutineId = 1 },
                new RoutineSet { SetId = 3, RoutineId = 1 },

                new RoutineSet { SetId = 4, RoutineId = 2 },
                new RoutineSet { SetId = 5, RoutineId = 2 },
                new RoutineSet { SetId = 6, RoutineId = 2 },

                new RoutineSet { SetId = 7, RoutineId = 3 },
                new RoutineSet { SetId = 8, RoutineId = 3 },
                new RoutineSet { SetId = 9, RoutineId = 3 },

                new RoutineSet { SetId = 10, RoutineId = 4 },

                new RoutineSet { SetId = 11, RoutineId = 5 },
                new RoutineSet { SetId = 12, RoutineId = 5 },
                new RoutineSet { SetId = 13, RoutineId = 5 },

                new RoutineSet { SetId = 14, RoutineId = 6 },
                new RoutineSet { SetId = 15, RoutineId = 6 },
                new RoutineSet { SetId = 16, RoutineId = 6 },

                new RoutineSet { SetId = 17, RoutineId = 7 },
                new RoutineSet { SetId = 18, RoutineId = 7 },
                new RoutineSet { SetId = 19, RoutineId = 7 },

                new RoutineSet { SetId = 20, RoutineId = 8 }
            };
            return routineSets;
        }
    }
}
