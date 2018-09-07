using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace augmented_aspnet_backend.Models.Workout
{
    public class Session
    {
        [Key]
        [Display(Name = "Session Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }
        [Display(Name = "Session Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Session End Time")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Session Name")]
        public string Name { get; set; }
        [Display(Name = "Session Comment")]
        public string Comment { get; set; }
    }
}