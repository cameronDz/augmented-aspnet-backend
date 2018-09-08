using AugmentedAspnetBackend.Models.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AugmentedAspnetBackend.ViewModels.Workouts
{
    public class SessionsViewModel
    {
        public SessionsViewModel() { }

        public SessionsViewModel(Session session)
        {
            Name = session.Name;
            StartTime = session.StartTime;
            EndTime = session.EndTime;
            Comment = session.Comment;
        }

        public String Name { get; set; }
        public String Comment { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IEnumerable<RoutineViewModel> Routines { get; set; }

    }
}