using AugmentedAspnetBackend.DAL;
using AugmentedAspnetBackend.Models.Workout;
using AugmentedAspnetBackend.ViewModels.Workouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.Repositories
{
    public class SessionRepository
    {
        private WorkoutContext _context;

        public SessionRepository()
        {
            _context = new WorkoutContext();
        }

        public SessionRepository(WorkoutContext context)
        {
            _context = context;
        }

        public SessionsViewModel GetSessionsViewModel(int id)
        {
            if (_context.Sessions.Find(id) == null)
            {
                throw new Exception("Invalid Session ID.");
            }
            SessionsViewModel vm = new SessionsViewModel(_context.Sessions.Where(s => s.SessionId == id).First());
            List<RoutineViewModel> routineViewModels = new List<RoutineViewModel>();
            foreach (Routine routine in _context.Routines.Where(r => r.SessionId == id))
            {
                RoutineViewModel routineVm = new RoutineViewModel(routine);
                if(_context.Exercises.Find(routine.ExerciseId) == null)
                {
                    throw new Exception("Invalid Exercise ID.");
                }
                Exercise exercise = _context.Exercises.Where(e => e.ExerciseId == routine.ExerciseId).First();
                ExerciseViewModel exerciseVm = new ExerciseViewModel(exercise);
                if (_context.ExerciseTypes.Find(exercise.TypeId) == null)
                {
                    throw new Exception("Invalid Exercise Type ID.");
                }
                exerciseVm.Type = _context.ExerciseTypes.Where(s => s.ExercisTypeId == exercise.TypeId).First().Name;
                routineVm.Exercise = exerciseVm;
                List<SetViewModel> setViewModels = new List<SetViewModel>();
                foreach(RoutineSet routineSet in _context.RoutineSets.Where(r => r.RoutineId == routine.RoutineId))
                {
                    if(_context.Sets.Find(routineSet.SetId) == null)
                    {
                        throw new Exception("Invalid Set ID.");
                    }
                    setViewModels.Add(new SetViewModel(_context.Sets.Where(s => s.SetId == routineSet.SetId).First()));
                }
                routineVm.Sets = setViewModels;
                routineViewModels.Add(routineVm);
            }
            vm.Routines = routineViewModels;
            return vm;
        }
    }
}
